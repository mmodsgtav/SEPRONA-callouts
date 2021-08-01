using LSPD_First_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using SEPRONA_Callouts;
using Rage.Native;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting.Entities;
using System.Xml;

namespace SEPRONA_Callouts.Callouts
{
    [CalloutInfo("Animal muerto en la calzada", CalloutProbability.Medium)]
    public class AnimalMuerto : Callout
    {
        public Vector3 ubicacion;
        public float[] x;
        public float[] y;
        public float[] z;
        public bool hascalloutbeenaccepted;
        public Ped animal;
        public Blip marca;
        public bool shown;
        public string[] animales;
        public override bool OnBeforeCalloutDisplayed()
        {
            hascalloutbeenaccepted = false;
            x = new float[]{-1482.77f, -802.63f, 3031.90f, 2064.91f, -539.28f, 269.02f, 471.68f};
            y = new float[] {1773.30f, 4052.21f, 5045.42f, 5205.23f, 2675.43f, 3137.02f, 3582.16f};
            z = new float[] { 87.14f, 160.23f, 26.35f, 54.40f, 45.40f, 42.03f, 33.35f};
            int num;
            num = SEPRONA_Callouts.Api.randomNum(0, x.Length);
            Game.LogTrivial("[Seprona CALLOUTS] Spawn number: " + num);
            ubicacion = new Vector3(x[num], y[num], z[num]);
            CalloutMessage = "Animal muerto en la calzada";
            CalloutPosition = ubicacion;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_DEAD_BODY_01 IN_OR_ON_POSITION", ubicacion);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            animales = new string[] { "a_c_pig", "a_c_cow", "a_c_boar", "a_c_rabbit_01" };
            int num;
            num = Api.randomNum(0, animales.Length);
            animal = new Ped(animales[num], ubicacion, 135f);
            hascalloutbeenaccepted = true;
            SEPRONA_Callouts.Api.displayMessage("Cadaver de animal en mitad de la calzada", "Ha aparecido un cadaver de animal en medio de la calzada, dirígete allí, limpia la calzada e investiga la escena.");
            marca = new Blip(animal.Position, 100f);
            marca.Alpha = 0.5f;
            marca.Color = Color.Yellow;
            shown = false;
            animal.Kill();
            marca.EnableRoute(Color.Yellow);
            return base.OnCalloutAccepted();
        }
        public override void Process()
        {

            if (Game.LocalPlayer.Character.Position.DistanceTo(animal.Position) <= 3f && shown == false)
            {
                Game.DisplayHelp("Manten pulsada la tecla " + Keys.X + " para retirar el cadaver de la carretera.", false);
                shown = true;
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(animal.Position) <= 3f)
            {
                while (Game.IsKeyDown(Keys.X))
                {
                    GameFiber.Wait(5000);
                    animal.Delete();
                }
            }
            if (Game.IsKeyDown(Keys.End) || !animal.Exists())
            {
                End();
            }
            base.Process();
        }
        public override void End()
        {
            if (hascalloutbeenaccepted) 
            {
                if (marca.Exists()) { marca.Delete(); }
                if (animal.Exists()) { animal.Delete(); }
                Api.Acabar();

            }
            base.End();
        }
    }
}
