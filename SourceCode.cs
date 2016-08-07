using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using GTA;
using GTA.Native;
using NativeUI;

public class MenuExample : Script
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
                UI.Notify("~b~You are now on duty!");
                Game.Player.ChangeModel("S_M_Y_USCG_01");
                Game.Player.Character.AddBlip();
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_NIGHTSTICK"), 1, false, false);
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_COMBATPISTOL"), 9999, false, false);
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_PUMPSHOTGUN"), 9999, false, false);
                Game.Player.Character.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_CARBINERIFLE"), 9999, false, false);
            }
        };

        //go on duty
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
                UI.Notify(text += ": show me avaible for calls");
            }
        };

        //10-7
        var notavaible = new UIMenuItem("Update Status As 10-7", "Notify dispatch that you are not avaible for calls!");
        dispatchmenu.AddItem(notavaible);
        dispatchmenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == notavaible)
            {
                UI.Notify(text += ": show me unavaible for calls");
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

        //add nightstick
        // pedda.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "NIGHTSTICK"), 1, false, false);
    }

    public void VehicleMenu(UIMenu menu)
    {
        var subvehiclemenu = _menuPool.AddSubMenu(menu, "Vehicle Menu");
        for (int i = 0; i < 1; i++) ;

        //Spawn Car
        //Vehicle car = World.CreateVehicle("LGUARD", Player.Character.Position, 30);
        var coastguardcar = new UIMenuItem("Coast Guard Car", "");
        subvehiclemenu.AddItem(coastguardcar);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == coastguardcar)
            {
                Vehicle car = World.CreateVehicle("LGUARD", Game.Player.Character.Position, 500);
            }
        };

        //Spawn beachvehicle
        //Vehicle car = World.CreateVehicle("LGUARD", Player.Character.Position, 30);
        var beachvehicle = new UIMenuItem("Utility Response Vehicle", "");
        subvehiclemenu.AddItem(beachvehicle);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == beachvehicle)
            {
                Vehicle car = World.CreateVehicle("CONTENDER", Game.Player.Character.Position, 500);
            }
        };

        //Spawn Modded SUV
        //SHERIFF
        var coastguardSUV = new UIMenuItem("Coast Guard SUV", "");
        subvehiclemenu.AddItem(coastguardSUV);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == coastguardSUV)
            {
                Vehicle car = World.CreateVehicle("SHERIFF2", Game.Player.Character.Position, 500);
            }
        };

        //Spawn ATV
        //Vehicle car = World.CreateVehicle("ADDER", Player.Character.Position, 30);
        var coastguardatv = new UIMenuItem("Coast Guard ATV", "");
        subvehiclemenu.AddItem(coastguardatv);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == coastguardatv)
            {
                Vehicle car = World.CreateVehicle("BLAZER2", Game.Player.Character.Position, 500);
            }
        };

        //Spawn boat2
        //Vehicle car = World.CreateVehicle("ADDER", Player.Character.Position, 30);
        var coastguardboat2 = new UIMenuItem("Coast Guard Boat", "");
        subvehiclemenu.AddItem(coastguardboat2);
        subvehiclemenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == coastguardboat2)
            {
                Vehicle car = World.CreateVehicle("DINGHY2", Game.Player.Character.Position, 500);
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
                Vehicle car = World.CreateVehicle("MAVERICK", Game.Player.Character.Position, 500);
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
    }

    public MenuExample()
    {
        _menuPool = new MenuPool();
        var mainMenu = new UIMenu("Coast Guard", "~b~Mod by Abel Gaming");
        _menuPool.Add(mainMenu);
        DutyMenu(mainMenu);
        DispatchMenu(mainMenu);
        PlayerMenu(mainMenu);
        VehicleMenu(mainMenu);
        VehicleOptions(mainMenu);
        _menuPool.RefreshIndex();

        Tick += (o, e) => Game.Player.WantedLevel = 0;
        Tick += (o, e) => _menuPool.ProcessMenus();
        KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.F5 && !_menuPool.IsAnyMenuOpen()) // Our menu on/off switch
                mainMenu.Visible = !mainMenu.Visible;
        };
    }
}
