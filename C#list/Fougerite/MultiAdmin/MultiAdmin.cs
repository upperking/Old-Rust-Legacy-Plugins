using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Fougerite.Events;
using Fougerite;
using UnityEngine;
using Player = Fougerite.Player;

namespace MultiAdmin
{
    public class MultiAdmin : Fougerite.Module
    {
        private IniParser Settings;
        public bool ShouldLogChat;
        public bool UseAdminTeleport;
        public bool UseAdminLoadout;
        public bool UsebanCommand;
        public bool UseVanishCommand;
        public bool UseAdminDoor;
        public bool AdminDoor;
        public bool UseKickCommand;
        public bool UseFreezeCommand;
        //public bool InvisIsOn;
        //public bool VanishOn;
        public bool LoadoutEnabled;
        public bool UseDrugCommand;
        //anti Abuse settings
        public bool ShouldPreventAbuse;
        public bool PreventVanishBug;
        public bool TurnAdminDoorsOffOnLeave;
        public bool ShouldRemoveLoadoutItemsAtConnect;
        //End
        public static string ppath;
        public static System.IO.StreamWriter file;
        public string pathfile = Directory.GetCurrentDirectory() + "\\save\\MultiAdmin\\Chat.log";
        public override string Name { get { return "MultiAdmin"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "The Most Advanced Admin System"; } }
        public override Version Version { get { return new Version("1.1.6"); } }

        private string green = "[color #82FA58]";
        private string yellow = "[color #F4FA58]";
        private string red = "[color #FF0000]";

        public override void Initialize()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "Chat.log"))) { File.Create(Path.Combine(ModuleFolder, "Chat.log")).Dispose(); }
            ppath = Path.Combine(ModuleFolder, "Chat.log");
            if (!File.Exists(Path.Combine(ModuleFolder, "Settings.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Settings.ini")).Dispose();
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                Settings.AddSetting("Settings", "ShouldLogChat", "true");
                Settings.AddSetting("Settings", "UseAdminTeleport", "true");
                Settings.AddSetting("Settings", "UseAdminLoadout", "true");
                Settings.AddSetting("Settings", "UseVanishCommand", "true");
                Settings.AddSetting("Settings", "UseAdminDoor", "true");
                Settings.AddSetting("Settings", "UsebanCommand", "true");
                Settings.AddSetting("Settings", "UseKickCommand", "true");
                Settings.AddSetting("Settings", "UseDrugCommand", "true");
                Settings.AddSetting("Settings", "UseFreezeCommand", "true");
                Settings.AddSetting("AntiAbuse", "ShouldPreventAbuse", "true");
                Settings.AddSetting("AntiAbuse", "PreventVanishBug", "true");
                Settings.AddSetting("AntiAbuse", "TurnAdminDoorsOffOnLeave", "true");
                Settings.AddSetting("AntiAbuse", "ShouldRemoveLoadoutItemsAtConnect", "true");

                Settings.Save();
            }
            else
            {
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                ShouldLogChat = bool.Parse(Settings.GetSetting("Settings", "ShouldLogChat"));
                UseAdminTeleport = bool.Parse(Settings.GetSetting("Settings", "UseAdminTeleport"));
                UseAdminLoadout = bool.Parse(Settings.GetSetting("Settings", "UseAdminLoadout"));
                UseVanishCommand = bool.Parse(Settings.GetSetting("Settings", "UseVanishCommand"));
                UseAdminDoor = bool.Parse(Settings.GetSetting("Settings", "UseAdminDoor"));
                UsebanCommand = bool.Parse(Settings.GetSetting("Settings", "UsebanCommand"));
                UseKickCommand = bool.Parse(Settings.GetSetting("Settings", "UseKickCommand"));
                ShouldPreventAbuse = bool.Parse(Settings.GetSetting("AntiAbuse", "ShouldPreventAbuse"));
                PreventVanishBug = bool.Parse(Settings.GetSetting("AntiAbuse", "PreventVanishBug"));
                TurnAdminDoorsOffOnLeave = bool.Parse(Settings.GetSetting("AntiAbuse", "TurnAdminDoorsOffOnLeave"));
                ShouldRemoveLoadoutItemsAtConnect = bool.Parse(Settings.GetSetting("AntiAbuse", "ShouldRemoveLoadoutItemsAtConnect"));
                UseDrugCommand = bool.Parse(Settings.GetSetting("Settings", "UseDrugCommand"));
                UseFreezeCommand = bool.Parse(Settings.GetSetting("Settings", "UseFreezeCommand"));
            }

            ReloadSettings();
            Fougerite.Hooks.OnCommand += OnCommand;
            Fougerite.Hooks.OnChat += OnChat;
            Hooks.OnDoorUse += new Hooks.DoorOpenHandlerDelegate(On_DoorUse);
            Fougerite.Hooks.OnPlayerDisconnected += OnPlayerDisconnected;
            Fougerite.Hooks.OnPlayerSpawned += On_Spawn;
            Fougerite.Hooks.OnPlayerKilled += OnPlayerKilled;
        }
        public override void DeInitialize()
        {
            ReloadSettings();
            Fougerite.Hooks.OnCommand -= OnCommand;
            Fougerite.Hooks.OnChat -= OnChat;
            Hooks.OnDoorUse -= new Hooks.DoorOpenHandlerDelegate(On_DoorUse);
            Fougerite.Hooks.OnPlayerDisconnected -= OnPlayerDisconnected;
            Fougerite.Hooks.OnPlayerSpawned -= On_Spawn;
            Fougerite.Hooks.OnPlayerKilled -= OnPlayerKilled;
        }
        public void OnCommand(Fougerite.Player player, string cmd, string[] args)
        {
            if (cmd == "madmin")
            {
                if (!player.Admin)
                {
                    player.Notice("✘", "You dont have permission to use this command");
                }
                else
                {
                    player.MessageFrom("MultiAdmin", green + "[color green]" + Name + " [color green]Version " + Version + " [color aqua]by [color yellow]" + Author);
                    player.MessageFrom("MultiAdmin", green + "/invis - gives you invisible suit");
                    player.MessageFrom("MultiAdmin", green + "/invisoff - Removes invis suit");
                    player.MessageFrom("MultiAdmin", green + "/cleaninv - cleans your inventory");
                    player.MessageFrom("MultiAdmin", green + "/atp name - teleports you");
                    player.MessageFrom("MultiAdmin", green + "/loadout  - gives you the admin loadout");
                    player.MessageFrom("MultiAdmin", green + "/vanish - Turns you into a ghost");
                    player.MessageFrom("MultiAdmin", green + "/adon - Turns Admindoors on");
                    player.MessageFrom("MultiAdmin", green + "/adoff - Turns Admindoors off");
                    player.MessageFrom("MultiAdmin", green + "/mban <Name> - Bans the player");
                    player.MessageFrom("MultiAdmin", green + "/mkick <Name> - kicks the player");
                    player.MessageFrom("MultiAdmin", green + "/drug <Name> - Drug the player");
                    player.MessageFrom("MultiAdmin", green + "/itemboost - Boost your current item to max stack");
                }
            }
            else if (cmd == "itemboost")
            {
                if (!player.Admin)
                {
                    player.Notice("✘", "You dont have permission to use this command");
                }
                else
                {
                    player.Inventory.InternalInventory.activeItem.SetUses(250);
                    player.MessageFrom("MultiAdmin", green + "Current item boosted");
                }
            }
            else if (cmd == "invis")
            {
                if (!player.Admin)
                {
                    player.Notice("✘", "You dont have permission to use this command");
                }
                else
                {
                    DataStore.GetInstance().Add("invis", "UseInvis", player.SteamID);
                    player.Inventory.RemoveItem(36);
                    player.Inventory.RemoveItem(37);
                    player.Inventory.RemoveItem(38);
                    player.Inventory.RemoveItem(39);
                    player.Inventory.AddItemTo("Invisible Helmet", 36, 1);
                    player.Inventory.AddItemTo("Invisible Vest", 37, 1);
                    player.Inventory.AddItemTo("Invisible Pants", 38, 1);
                    player.Inventory.AddItemTo("Invisible Boots", 39, 1);
                    player.MessageFrom("MultiAdmin", green + "None can see you now!");
                    Logger.Log(player.Name + " Has spawned himself a invisible suit");
                }
            }
            else if (cmd == "invisoff")
            {
                if (!player.Admin)
                {
                    player.Notice("✘", "You dont have permission to use this command");
                }
                else
                {
                    DataStore.GetInstance().Remove("invis", player.SteamID);
                    player.Inventory.RemoveItem(36);
                    player.Inventory.RemoveItem(37);
                    player.Inventory.RemoveItem(38);
                    player.Inventory.RemoveItem(39);
                    player.MessageFrom("MultiAdmin", "You are not a god anymore!");
                    //InvisIsOn = false;
                }
            }
            else if (cmd == "cleaninv")
            {
                if (!player.Admin)
                {
                    player.Notice("✘", "You dont have permission to use this command");
                }
                else
                {
                    player.Inventory.ClearAll(); // this cleans the inventory
                    player.Notice("☤", "Inventory cleared!");
                    Logger.Log(player.Name + " has cleaned his inventory");
                }
            }
            else if (cmd == "atp")
            {
                if (UseAdminTeleport)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else
                    {
                        string s = string.Join(" ", args);
                        Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                        if (p == null)
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Failed to find " + s);
                        }
                        else if (args.Length.Equals(0))
                        {
                            player.MessageFrom("MultiAdmin", green + "Wrong Syntax use /atp <name>");
                        }
                        else
                        {
                            Fougerite.Player target = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                            if (!target.Equals(null))
                            {
                                float x = target.X;
                                float y = target.Y;
                                float z = target.Z;
                                player.TeleportTo(target.X, target.Y, target.Z);
                                player.MessageFrom("MultiAdmin", green + "You have been teleported to " + target.Name);
                                Logger.Log(player.Name + " Has Atped to " + target.Name);
                            }
                        }
                    }
                }
            }
            else if (cmd == "loadout") //make sure you disabled the loadoad feature in rust++ or disable this command in settings.ini
            {
                if (UseAdminLoadout)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else
                    {
                        player.Inventory.RemoveItem(36);
                        player.Inventory.RemoveItem(37);
                        player.Inventory.RemoveItem(38);
                        player.Inventory.RemoveItem(39);
                        player.Inventory.AddItemTo("Kevlar Helmet", 36, 1);
                        player.Inventory.AddItemTo("Kevlar Vest", 37, 1);
                        player.Inventory.AddItemTo("Kevlar Pants", 38, 1);
                        player.Inventory.AddItemTo("Kevlar Boots", 39, 1);
                        player.Inventory.AddItem("M4", 1);
                        player.Inventory.AddItem("P250", 1);
                        player.Inventory.AddItem("556 Ammo", 250);
                        player.Inventory.AddItem("9mm Ammo", 250);
                        Logger.Log(player.Name + " Has used the /loadout command");
                        DataStore.GetInstance().Add("Loadout", player.SteamID, "Loadout");
                    }
                }
            }
            else if (cmd == "multiadmin")
            {
                if (!player.Admin)
                {
                    player.Notice("✘", "You dont have permission to use this command");
                }
                else
                {
                    ReloadSettings();
                    player.MessageFrom("MultiAdmin", green + "[color green]MultiAdmin [color aqua]by [color yellow]ice cold");
                    player.MessageFrom("MultiAdmin", green + "Settings have been reloaded");
                }
            }
            else if (cmd == "vanish")
            {
                if (UseVanishCommand)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else if (player.PlayerClient.controllable.health == 0.0)
                    {
                        player.Health = 100f;
                        player.Notice("Your soul has been returned into your body");
                    }
                    else
                    {
                        player.Health = (player.PlayerClient.controllable.health - player.PlayerClient.controllable.health);
                        player.Notice("You are now a ghosty boo");
                        DataStore.GetInstance().Add("Vanish", player.SteamID, "Vanish");
                    }
                }
            }
            else if (cmd == "adon")
            {
                if (UseAdminDoor)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else
                    {
                        player.MessageFrom("MultiAdmin", green + "Admin doors enabled");
                        DataStore.GetInstance().Add("Doors", player.SteamID, "1");
                    }
                }
            }
            else if (cmd == "adoff")
            {
                if (UseAdminDoor)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else
                    {
                        player.MessageFrom("MultiAdmin", green + "Admin doors disabled");
                        DataStore.GetInstance().Remove("Doors", player.SteamID);
                    }
                }
            }
            else if (cmd == "mban")
            {
                if (UsebanCommand)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else
                    {
                        string s = string.Join(" ", args);
                        Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                        if (p == null)
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Failed to find " + s);
                        }
                        else if (args.Length.Equals(0))
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Wrong Syntax use /mban <name>");
                        }
                        else
                        {
                            Fougerite.Player banned = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                            if (!banned.Equals(null))
                            {
                                Fougerite.Server.GetServer().BanPlayer(banned, "MultiAdminBan", "Was banned from the server");
                                player.Notice("☢", banned.Name + "Was banned from the server");
                                Fougerite.Server.GetServer().BroadcastNotice(banned.Name + "Has been banned from the server");
                            }
                        }
                    }
                }
            }
            else if (cmd == "mkick")
            {
                if (UseKickCommand)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else
                    {
                        string s = string.Join(" ", args);
                        Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                        if (p == null)
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Failed to find " + s);
                        }
                        else if (args.Length.Equals(0))
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Wrong Syntax use /mkick <name>");
                        }
                        else
                        {
                            Fougerite.Player kicked = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                            if (!kicked.Equals(null))
                            {
                                player.Notice("☢", kicked.Name + "Was kicked from the server");
                                Fougerite.Server.GetServer().BroadcastNotice(kicked.Name + "Has been kicked from the server");
                                kicked.Disconnect();
                            }
                        }
                    }
                }
            }
            else if (cmd == "drug")
            {
                if (UseDrugCommand)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else
                    {
                        string s = string.Join(" ", args);
                        Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                        if (p == null)
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Failed to find " + s);
                        }
                        else if (args.Length.Equals(0))
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Wrong Syntax use /drug <name>");
                        }
                        else
                        {
                            Fougerite.Player pl = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                            if (!pl.Equals(null))
                            {
                                player.Notice("☢", pl.Name + "Was drugged");
                                pl.SendCommand("render.fov 120");
                            }
                        }
                    }
                }
            }
            else if (cmd == "freeze")
            {
                if (UseFreezeCommand)
                {
                    if (!player.Admin)
                    {
                        player.Notice("✘", "You dont have permission to use this command");
                    }
                    else
                    {
                        string s = string.Join(" ", args);
                        Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                        if (p == null)
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Failed to find " + s);
                        }
                        else if (args.Length.Equals(0))
                        {
                            player.MessageFrom("MultiAdmin", yellow + "Wrong Syntax use /freeze <name>");
                        }
                        else
                        {
                            Fougerite.Player pl = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                            if (!pl.Equals(null))
                            {
                                player.Notice("☢", pl.Name + " Was frozen");
                                pl.SendCommand("input.bind Left None None");
                                pl.SendCommand("input.bind Right None None");
                                pl.SendCommand("input.bind Up None None");
                                pl.SendCommand("input.bind Down None None");
                            }
                        }
                    }
                }
            }
        }
        public void OnChat(Fougerite.Player player, ref ChatString text)
        {
            if (ShouldLogChat)
            {
                string line = DateTime.Now + " " + player.Name + ": " + text + " ";
                file = new System.IO.StreamWriter(ppath, true);
                file.WriteLine(line);
                file.Close();
            }
        }
        private void ReloadSettings()
        {
            Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
            {
                ShouldLogChat = bool.Parse(Settings.GetSetting("Settings", "ShouldLogChat"));
                UseAdminTeleport = bool.Parse(Settings.GetSetting("Settings", "UseAdminTeleport"));
                UseAdminLoadout = bool.Parse(Settings.GetSetting("Settings", "UseAdminLoadout"));
                UseVanishCommand = bool.Parse(Settings.GetSetting("Settings", "UseVanishCommand"));
                UseAdminDoor = bool.Parse(Settings.GetSetting("Settings", "UseAdminDoor"));
                UsebanCommand = bool.Parse(Settings.GetSetting("Settings", "UsebanCommand"));
                ShouldLogChat = Settings.GetBoolSetting("Settings", "ShouldLogChat");
                UseAdminTeleport = Settings.GetBoolSetting("Settings", "UseAdminTeleport");
                UseAdminLoadout = Settings.GetBoolSetting("Settings", "UseAdminLoadout");
                UseVanishCommand = Settings.GetBoolSetting("Settings", "UseVanishCommand");
                UseAdminDoor = Settings.GetBoolSetting("Settings", "UseAdminDoor");
                UsebanCommand = Settings.GetBoolSetting("Settings", "UsebanCommand");
                UseKickCommand = Settings.GetBoolSetting("Settings", "UseKickCommand");
            }
        }
        public void On_DoorUse(Fougerite.Player player, Fougerite.Events.DoorEvent DoorEvent)
        {
            if (DataStore.GetInstance().ContainsKey("Doors", player.SteamID))
            {
                if (player.Admin)
                {
                    DoorEvent.Open = true;
                }
            }
        }
        public void OnPlayerDisconnected(Fougerite.Player player)
        {
            if (ShouldPreventAbuse)
            {
                if (DataStore.GetInstance().ContainsKey("Invis", player.SteamID))
                {
                    DataStore.GetInstance().Remove("Invis", player.SteamID);
                }
                else if (TurnAdminDoorsOffOnLeave)
                {
                    if (DataStore.GetInstance().ContainsKey("Doors", player.SteamID))
                    {
                        DataStore.GetInstance().Remove("Doors", player.SteamID);
                    }
                }
            }
        }
        public void On_Spawn(Fougerite.Player player, SpawnEvent e)
        {
            if (ShouldRemoveLoadoutItemsAtConnect)
            {
                if (LoadoutEnabled == true)
                {
                    player.Inventory.RemoveItem(36);
                    player.Inventory.RemoveItem(37);
                    player.Inventory.RemoveItem(38);
                    player.Inventory.RemoveItem(39);
                    player.Inventory.RemoveItem("M4", 1);
                    player.Inventory.RemoveItem("P250", 1);
                    player.Inventory.RemoveItem("556 Ammo", 250);
                    player.Inventory.RemoveItem("9mm Ammo", 250);
                    LoadoutEnabled = false;
                }
            }
            else if (PreventVanishBug)
            {
                if (DataStore.GetInstance().ContainsKey("Vanish", player.SteamID))
                {
                    DataStore.GetInstance().Remove("Vanish", player.SteamID);
                    player.Health += 100f;
                }
            }
        }
        public void OnPlayerKilled(DeathEvent de)
        {
            if (DataStore.GetInstance().ContainsKey("invis", "UseInvis"))
            {
                Fougerite.Server.GetServer().BroadcastNotice(de.Victim.Name + "Was killed by admin abuse");
            }
        }
    }
}