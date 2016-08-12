//Version 0.3
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using GTA;
using GTA.Native;
using NativeUI;

public class CoastGuardMod : Script
{
    ScriptSettings config;
    string text;
    private Ped playerPed = Game.Player.Character;
    private Player player = Game.Player;
    private MenuPool _menuPool;

    public void DutyMenu(UIMenu menu)
    {
        var dutymenu = _menuPool.AddSubMenu(menu, "Go On/Off Duty");
        for (int i = 0; i < 1; i++) ;

        //go on duty
        var goonduty = new UIMenuItem("Go On Duty", "Click to go on duty");
        dutymenu.AddItem(goonduty);
        dutymenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == goonduty)
            {
                Game.Player.Character.Position = new GTA.Math.Vector3(-752.742f, -1508.286f, 5.008f);
                Game.Player.CanControlCharacter = false;
                var station = new GTA.Math.Vector3(-752.742f, -1508.286f, 5.008f);
                var stationblip = World.CreateBlip(station);
                var station2 = new GTA.Math.Vector3(2836.699f, -641.453f, 1.633f);
                var station2blip = World.CreateBlip(station2);
                stationblip.Sprite = BlipSprite.PoliceStation;
                station2blip.Sprite = BlipSprite.PoliceStation;
                Game.FadeScreenOut(5000);
                Wait(3000);
                Game.FadeScreenIn(5000);
                UI.Notify("~b~You are now on duty!");
                Game.Player.ChangeModel("S_M_Y_USCG_01");
                Game.Player.Character.AddBlip();
                Game.Player.Character.CurrentBlip.Sprite = BlipSprite.PoliceArea;
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_NIGHTSTICK"), 1, false, false);
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_FLASHLIGHT"), 1, false, false);
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_STUNGUN"), 9999, false, false);
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_COMBATPISTOL"), 9999, false, false);
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_PUMPSHOTGUN"), 9999, false, false);
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_CARBINERIFLE"), 9999, false, false);
                Game.Player.CanControlCharacter = true;
            }
        };

        //go off duty
        var gooffduty = new UIMenuItem("Go Off Duty", "Click to go off duty");
        dutymenu.AddItem(gooffduty);
        dutymenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == gooffduty)
            {
                UI.Notify("~r~You are now off duty!");
                Game.Player.ChangeModel("PLAYER_ZERO");
                Game.Player.Character.CurrentBlip.Remove();
                Game.Player.Character.Weapons.RemoveAll();
                Game.Player.Character.Position = new GTA.Math.Vector3(-136.517f, 870.754f, 232.693f);
            }
        };
    }

    public void DispatchMenu(UIMenu menu)
    {
        config = ScriptSettings.Load("scripts\\unit.ini");
        text = config.GetValue<string>("Options", "Unit", "141");
        var dispatchmenu = _menuPool.AddSubMenu(menu, "Dispatch Menu");
        for (int i = 0; i < 1; i++) ;

        //10-8
        var avaible = new UIMenuItem("Update Status As 10-8", "Notify dispatch that you are avaible for calls!");
        dispatchmenu.AddItem(avaible);
        dispatchmenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == avaible)
            {
                UI.ShowSubtitle("~r~Sorry, I'm still working on this, ~g~but you can force calls from the call-out menu! :P");
            }
        };

        //10-7
        var notavaible = new UIMenuItem("Update Status As 10-7", "Notify dispatch that you are not avaible for calls!");
        dispatchmenu.AddItem(notavaible);
        dispatchmenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == notavaible)
            {
                UI.Notify("~g~Control: ~w~10-4 Coast Guard One, showing you 10-7");
            }
        };
    }

    public void PlayerMenu(UIMenu menu)
    {
        var subplayermenu = _menuPool.AddSubMenu(menu, "Player Menu");
        for (int i = 0; i < 1; i++) ;
        
        //heal player
        var healplayer = new UIMenuItem("Heal Player", "");
        subplayermenu.AddItem(healplayer);
        subplayermenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == healplayer)
            {
                Game.Player.Character.Health = 100;
            }
        };

        //add armour
        var armour = new UIMenuItem("Add Armour", "");
        subplayermenu.AddItem(armour);
        subplayermenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == armour)
            {
                Game.Player.Character.Armor = 100;
            }
        };

        //Medic
        //S_M_M_PARAMEDIC_01
        var pmedic = new UIMenuItem("Paramedic Model", "Switch to Coast Guard Paramedic");
        subplayermenu.AddItem(pmedic);
        subplayermenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == pmedic)
            {
                Game.Player.ChangeModel("S_M_M_PARAMEDIC_01");
            }
        };
    }

    public void VehicleMenu(UIMenu menu)
    {
        var subvehiclemenu = _menuPool.AddSubMenu(menu, "Vehicle Menu");
        for (int i = 0; i < 1; i++) ;

        //Spawn Car
        var coastguardcar = new UIMenuItem("Coast Guard Car", "");
        subvehiclemenu.AddItem(coastguardcar);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == coastguardcar)
            {
                Vehicle car = World.CreateVehicle("SHERIFF2", World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(10f)));
                car.AddBlip();
                car.CurrentBlip.Color = BlipColor.Blue;
            }
        };

        //Spawn contender
        var beachvehicle = new UIMenuItem("Utility Response Vehicle", "");
        subvehiclemenu.AddItem(beachvehicle);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == beachvehicle)
            {
                Vehicle car2 = World.CreateVehicle("CONTENDER", World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(10f)));
                car2.AddBlip();
                car2.CurrentBlip.Color = BlipColor.Blue;
                car2.NumberPlate = "GUARD";
                car2.CustomPrimaryColor = Color.OrangeRed;
                car2.CustomSecondaryColor = Color.Gray;
            }
        };

        //Spawn ATV
        var coastguardatv = new UIMenuItem("Coast Guard ATV", "");
        subvehiclemenu.AddItem(coastguardatv);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == coastguardatv)
            {
                Vehicle car3 = World.CreateVehicle("BLAZER2", World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(10f)));
                car3.AddBlip();
                car3.CurrentBlip.Color = BlipColor.Blue;
            }
        };

        //Spawn boat2
        var coastguardboat2 = new UIMenuItem("Coast Guard Boat", "");
        subvehiclemenu.AddItem(coastguardboat2);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == coastguardboat2)
            {
                Vehicle car4 = World.CreateVehicle("DINGHY2", Game.Player.Character.Position, 500);
                car4.AddBlip();
                car4.CurrentBlip.Sprite = BlipSprite.Boat;
            }
        };

        //Jet Ski
        var coastguardboat = new UIMenuItem("Coast Guard Jet Ski", "");
        subvehiclemenu.AddItem(coastguardboat);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == coastguardboat)
            {
                Vehicle car5 = World.CreateVehicle("SEASHARK2", Game.Player.Character.Position, 500);
                car5.AddBlip();
                car5.CurrentBlip.Sprite = BlipSprite.Boat;
            }
        };

        //Spawn helicoper
        //MAVERICK
        var helicopter = new UIMenuItem("Coast Guard Helicopter", "");
        subvehiclemenu.AddItem(helicopter);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == helicopter)
            {
                Vehicle car6 = World.CreateVehicle("MAVERICK", Game.Player.Character.Position, 500);
                car6.AddBlip();
                car6.CurrentBlip.Sprite = BlipSprite.Helicopter;
            }
        };

        //DELETE BLIPS
        var deletecarblips = new UIMenuItem("Delete Car Blips", "");
        subvehiclemenu.AddItem(deletecarblips);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == deletecarblips)
            {
                UI.ShowSubtitle("Sorry, I'm still working on this :P");
            }
        };
    }

    public void VehicleOptions(UIMenu menu)
    {
        var subvehicleoptions = _menuPool.AddSubMenu(menu, "Vehicle Options");
        for (int i = 0; i < 1; i++) ;

        //Repair
        var repair = new UIMenuItem("Repair Vehicle", "");
        subvehicleoptions.AddItem(repair);
        subvehicleoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == repair)
            {
                Game.Player.Character.CurrentVehicle.Repair();
            }
        };

        //Open Trunk
        var opentrunk = new UIMenuItem("Open Trunk", "");
        subvehicleoptions.AddItem(opentrunk);
        subvehicleoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == opentrunk)
            {
                Game.Player.Character.CurrentVehicle.OpenDoor(VehicleDoor.Trunk, false, true);
            }
        };

        //Close Trunk
        var closetrunk = new UIMenuItem("Close Trunk", "~r~Warning: This will repair your vehicle. Code is bugged!");
        subvehicleoptions.AddItem(closetrunk);
        subvehicleoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == closetrunk)
            {
                Game.Player.Character.CurrentVehicle.Repair();
            }
        };
    }

    public void TeleportMenu(UIMenu menu)
    {
        var teleportoptions = _menuPool.AddSubMenu(menu, "Teleport Menu");
        for (int i = 0; i < 1; i++) ;

        //
        var stationbase = new UIMenuItem("Main Station", "Southern Los Santos");
        teleportoptions.AddItem(stationbase);
        teleportoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == stationbase)
            {
                Game.Player.Character.Position = new GTA.Math.Vector3(-752.742f, -1508.286f, 5.008f);
            }
        };

        //
        var secondarybase = new UIMenuItem("Secondary Station", "Palomino Highlands");
        teleportoptions.AddItem(secondarybase);
        teleportoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == secondarybase)
            {
                Game.Player.Character.Position = new GTA.Math.Vector3(2836.699f, -641.453f, 1.633f);
            }
        };

        //
        var headquarters = new UIMenuItem("Headquarters", "");
        teleportoptions.AddItem(headquarters);
        teleportoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == headquarters)
            {
                Game.Player.Character.Position = new GTA.Math.Vector3(-892.503f, -444.700f, 125.132f);
            }
        };

        //
        var fzancudoact = new UIMenuItem("Fort Zancudo Air Control Tower", "");
        teleportoptions.AddItem(fzancudoact);
        teleportoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == fzancudoact)
            {
                Game.Player.Character.Position = new GTA.Math.Vector3(-2360.731f, 3244.930f, 92.904f);
            }
        };

        //
        var sshoresact = new UIMenuItem("Sandy Shores Air Control Tower", "");
        teleportoptions.AddItem(sshoresact);
        teleportoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == sshoresact)
            {
                Game.Player.Character.Position = new GTA.Math.Vector3(1700.370f, 3292.059f, 48.922f);
            }
        };

        //
        var dcr = new UIMenuItem("Fort Zancudo Air Control Tower", "");
        teleportoptions.AddItem(dcr);
        teleportoptions.OnItemSelect += (sender, item, index) =>
        {
            if (item == dcr)
            {
                Game.Player.Character.Position = new GTA.Math.Vector3(568.861f, -3123.702f, 18.769f);
            }
        };
    }

    public void CallOutMenu(UIMenu menu)
    {
        //REMOVED THIS CODE FROM PUBLIC
    }

    public CoastGuardMod()
    {
        _menuPool = new MenuPool();
        var mainMenu = new UIMenu("Coast Guard", "~b~Mod by Abel Gaming");
        _menuPool.Add(mainMenu);
        DutyMenu(mainMenu);
        DispatchMenu(mainMenu);
        CallOutMenu(mainMenu);
        PlayerMenu(mainMenu);
        VehicleMenu(mainMenu);
        VehicleOptions(mainMenu);
        TeleportMenu(mainMenu);
        _menuPool.RefreshIndex();

        //REMOVED THIS CODE
        Tick += (o, e) => _menuPool.ProcessMenus();
        KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.F5 && !_menuPool.IsAnyMenuOpen()) // Our menu on/off switch
                mainMenu.Visible = !mainMenu.Visible;
        };
    }
}
