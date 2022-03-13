using LSPD_First_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage;
using Rage.Native;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Engine.Scripting.Entities;
using SEPRONA_Callouts;
using System.Net;
using System.Reflection;
using SEPRONA_Callouts.Callouts;

namespace SEPRONA_Callouts
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            LSPD_First_Response.Mod.API.Functions.OnOnDutyStateChanged += OnOnDutyStateChangedHandler;


            Game.LogTrivial("Seprona Callouts" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + " ha cargado correctamente.");
            if (SEPRONA_Callouts.Api.isPluginUptoDate())
            {
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "SEPRONA CALLOUTS", "Desarrollado por ~b~mmodsgtav~w~.", "Está ~g~actualizado~w~. Versión: ~g~" + Assembly.GetExecutingAssembly().GetName().Version);
            }
            else
            {
                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "SEPRONA CALLOUTS", "Desarrollado por ~b~mmodsgtav~w~.", "Hay una actualización del plugin disponible.");
            }
        }
        private static void OnOnDutyStateChangedHandler(bool OnDuty)
        {

            if (OnDuty)
            {

                RegisterCallouts();


                Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "Seprona Callouts", "Desarrollado por ~b~mmodsgtav~w~.", "Ha cargado ~g~correctamente~w~.");
            }
        }
        private static void RegisterCallouts()
        {
            LSPD_First_Response.Mod.API.Functions.RegisterCallout(typeof(AnimalMuerto));
            Functions.RegisterCallout(typeof(MenuTest));
        }

        public override void Finally()
        {
            Game.LogTrivial("Seprona Callouts ha sido limpiado correctamente.");
        }
    }
}
