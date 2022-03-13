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
using RAGENativeUI;
using RAGENativeUI.Elements;
using RAGENativeUI.PauseMenu;


namespace SEPRONA_Callouts.Callouts
{
    [CalloutInfo("Llamada de prueba", CalloutProbability.VeryLow)]
    public class MenuTest : Callout
    {
        public override bool OnBeforeCalloutDisplayed()
        {
            Vector3 newVector = new Vector3(Game.LocalPlayer.Character.Position.X, Game.LocalPlayer.Character.Position.Y, Game.LocalPlayer.Character.Position.Z);
            CalloutMessage = "Llamada de prueba";
            CalloutPosition = newVector;
            return base.OnBeforeCalloutDisplayed();
        }
        
        public override bool OnCalloutAccepted()
        {

            UIMenu menuBueno = MenuApi.createNewMenu();
            menuBueno.OnItemSelect += MenuBueno_OnItemSelect;
            return base.OnCalloutAccepted();
        }

        private void MenuBueno_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            Game.DisplayNotification("Has pulsado: " + selectedItem.Text);
        }

        public override void Process()
        {
            base.Process();
        }
        public override void End()
        {
            Api.Acabar();
            base.End();
        }
        
    }
}
