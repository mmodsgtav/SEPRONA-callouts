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
    public class CaravanasIlegales : Callout
    {
        private float[] _x;
        private float[] _y;
        private float[] _z;
        private float[] _heading;
        private Vector3 _Calloutspawn;
        private bool _hasCalloutBeenAccepted = false;
        private Blip _areaBlip;
        private Vehicle _caravana;
        private int num;
        private Ped _ped;
        public override bool OnBeforeCalloutDisplayed()
        {
            Random rnd = new Random();
            num = rnd.Next(0, _x.Length);
            _Calloutspawn = new Vector3(_x[num], _y[num], _z[num]);
            CalloutMessage = "Caravanas ilegales";
            CalloutPosition = _Calloutspawn;
            AddMinimumDistanceCheck(50f, Game.LocalPlayer.Character.Position);
            AddMaximumDistanceCheck(2000f, Game.LocalPlayer.Character.Position);
            ShowCalloutAreaBlipBeforeAccepting(_Calloutspawn, 100);
            return base.OnBeforeCalloutDisplayed();
        }
        public override bool OnCalloutAccepted()
        {
            _hasCalloutBeenAccepted = true;
            _areaBlip = new Blip(_Calloutspawn, 400f);
            _areaBlip.IsRouteEnabled = false;
            _areaBlip.Color = Color.Yellow;
            _areaBlip.Alpha = 0.5f;
            _areaBlip.Flash(1, 2);
            _caravana = new Vehicle("", _Calloutspawn, _heading[num]);
            _ped = new Ped(_Calloutspawn);
            _ped.Face(Game.LocalPlayer.Character);
            return base.OnCalloutAccepted();
        }
    }
}