using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Rage;
using System.Drawing;
using System.IO;
using LSPD_First_Response.Mod.API;
using Rage.Native;
using System.Reflection;
using RAGENativeUI;
using RAGENativeUI.Elements;
using RAGENativeUI.PauseMenu;
using System.Windows.Forms;

namespace SEPRONA_Callouts
{
    public class Api
    {
        
        public static bool internetAvailable()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    using (webClient.OpenRead("https://google.es/"))
                        return true;
                }
            }
            catch
            {
                return true;
            }
        }
        public static bool isPluginUptoDate()
        {
            if (internetAvailable())
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] url = wc.DownloadData("https://mmodsgtav.es/plugins/SepronaCallouts/currentversion");

                string webData = System.Text.Encoding.UTF8.GetString(url);
                if (Assembly.GetExecutingAssembly().GetName().Version + "" == webData)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static void displayMessage(string Title, string Description)
        {
            Game.DisplayNotification("sepro_callouts", "sepro_callouts", "SEPRONA", Title, Description);
        }
        public static int randomNum(int Num1, int Num2)
        {
            Random rnd;
            rnd = new Random();
            int definitivo;
            definitivo = rnd.Next(Num1, Num2);
            return definitivo;
        }
        public static void Acabar()
        {
            Game.DisplayNotification("sepro_callouts", "sepro_callouts", "SEPRONA CALLOUTS", "Código 4", "Servicio finalizado.");
            Functions.PlayScannerAudio("WE_ARE_CODE_4 NO_FURTHER_UNITS_REQUIRED");
        }
    }
    public class MenuApi
    {
        static MenuPool poolMenu = new MenuPool();
        static UIMenu myMenu = new UIMenu("Seprona Callouts", "~b~Menú de interacción");
        public static UIMenu createNewMenu()
        {
            var button = new UIMenuItem("Hablar con Rodolfo");
            var checkBox = new UIMenuCheckboxItem("Disponible", false);
            var lista = new UIMenuListScrollerItem<string>("Lista", "Descripción", new[] { "item1", "item2" });
            myMenu.AddItems(
                button,
                checkBox,
                lista
                );

            poolMenu.Add(myMenu);
            GameFiber.StartNew(procesarMenu);
            return myMenu;
        }
        private static void procesarMenu()
        {
            while (true)
            {
                GameFiber.Yield();
                poolMenu.ProcessMenus();

                if (Game.IsKeyDown(Keys.F2))
                {
                    if (myMenu.Visible)
                    {
                        myMenu.Visible = false;
                    }
                    else if (!UIMenu.IsAnyMenuVisible && !TabView.IsAnyPauseMenuVisible)
                    {
                        myMenu.Visible = true;
                    }
                }

            }
        }
    }
}
