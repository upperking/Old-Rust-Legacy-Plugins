using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using System.IO;
using Fougerite.Events;
using UnityEngine;
using RustProto;


namespace Ultimate_Freezer
{
    public class Ultimate_Freezer : Fougerite.Module
    {
        public override string Name { get { return "Ultimate Freezer"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "The best freezer plugin + Without Bypasses from players"; } }
        public override Version Version { get { return new Version("1.3"); } }
        private IniParser Settings;
        private string green = "[color #82FA58]";
        private string blue = "[color #76eec6]";
        private string red = "[color #FF0000]";
        public bool BanPlayerWhenLeave;
        public bool Announces;
        // using list is much faster then using datastore XD
        private List<string> Frozen = new List<string> { };

        public override void Initialize()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "Settings.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Settings.ini")).Dispose();
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                Settings.AddSetting("Settings", "BanPlayerWhenLeave", "false");
                Settings.AddSetting("Settings", "Announces", "true ");
                Settings.Save();
            }
            else
            {
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                BanPlayerWhenLeave = bool.Parse(Settings.GetSetting("Settings", "BanPlayerWhenLeave"));
                Announces = bool.Parse(Settings.GetSetting("Settings", "Announces"));
            }
            Fougerite.Hooks.OnPlayerMove += OnPlayerMove;
            Fougerite.Hooks.OnCommand += OnCommand;
            Fougerite.Hooks.OnPlayerConnected += OnPlayerConnected;
            Fougerite.Hooks.OnPlayerDisconnected += OnPlayerDisconnected;
            Fougerite.Hooks.OnPlayerHurt += PlayerHurt;
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnPlayerMove -= OnPlayerMove;
            Fougerite.Hooks.OnCommand -= OnCommand;
            Fougerite.Hooks.OnPlayerConnected -= OnPlayerConnected;
            Fougerite.Hooks.OnPlayerDisconnected -= OnPlayerDisconnected;
            Fougerite.Hooks.OnPlayerHurt -= PlayerHurt;
        }
        public void OnCommand(Fougerite.Player player, string cmd, string[] args)
        {
            if (cmd == "freeze")
            {
                if (!player.Admin)
                {
                    player.Notice("✘", "You are not allowed to freeze someone");
                }
                else
                {
                    string s = string.Join(" ", args);
                    Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                    if (p == null)
                    {
                        player.MessageFrom("Freezer", green + "Failed to find " + s);
                    }
                    else if (args.Length.Equals(0))
                    {
                        player.MessageFrom("Freezer", green + "Wrong Syntax use /freeze <name>");
                    }
                    else
                    {
                        Fougerite.Player pl = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                        if (!pl.Equals(null))
                        {
                            var id = pl.SteamID;
                            player.Notice("☢", pl.Name + " Was frozen");
                            pl.SendCommand("input.bind Left None None");
                            pl.SendCommand("input.bind Right None None");
                            pl.SendCommand("input.bind Up None None");
                            pl.SendCommand("input.bind Down None None");
                            pl.MessageFrom("Freezer", blue + "You has been frozen");
                            Frozen.Add(id);
                            if (Announces)
                            {
                                Fougerite.Server.GetServer().Broadcast(pl.Name + blue + " Has been frozen by " + green + player.Name);
                            }
                        }
                    }
                }
            }
            else if (cmd == "unfreeze")
            {
                if (!player.Admin)
                {
                    player.Notice("✘", "You are not allowed to unfreeze someone");
                }
                else
                {
                    string s = string.Join(" ", args);
                    Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                    if (p == null)
                    {
                        player.MessageFrom("Freezer", green + "Failed to find " + s);
                    }
                    else if (args.Length.Equals(0))
                    {
                        player.MessageFrom("Freezer", green + "Wrong Syntax use /unfreeze <name>");
                    }
                    else
                    {
                        Fougerite.Player pl = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                        if (!pl.Equals(null))
                        {
                            var id = pl.SteamID;
                            player.Notice("☢", pl.Name + " Was unfrozen");
                            pl.SendCommand("input.bind Left A None");
                            pl.SendCommand("input.bind Right D None");
                            pl.SendCommand("input.bind Up W None");
                            pl.SendCommand("input.bind Down S None");
                            pl.MessageFrom("Freezer", blue + "You has been unfrozen");
                            pl.InventoryNotice("You have been paralyzed");
                            Frozen.Remove(id);
                            if (Announces)
                            {
                                Fougerite.Server.GetServer().Broadcast(pl.Name + blue + " Has been unfrozen by " + green + player.Name);
                            }
                        }
                    }
                }
            }
        }
        public void OnPlayerMove(HumanController hc, Vector3 origin, int encoded, ushort stateflags, uLink.NetworkMessageInfo info, Util.PlayerActions action)
        {
            Fougerite.Player pl = Fougerite.Server.Cache.ContainsKey(hc.netUser.userID) ? Fougerite.Server.Cache[hc.netUser.userID] : Fougerite.Server.GetServer().FindPlayer(hc.netUser.userID.ToString());
            {
                var id = pl.SteamID;
                if (Frozen.Contains(id))
                {
                    pl.SendCommand("input.bind Left None None");
                    pl.SendCommand("input.bind Right None None");
                    pl.SendCommand("input.bind Up None None");
                    pl.SendCommand("input.bind Down None None");
                }
            }
        }

        public void OnPlayerConnected(Fougerite.Player pl)
        {
            var id = pl.SteamID;
            if (Frozen.Contains(id))
            {
                pl.SendCommand("input.bind Left None None");
                pl.SendCommand("input.bind Right None None");
                pl.SendCommand("input.bind Up None None");
                pl.SendCommand("input.bind Down None None");
                pl.MessageFrom("Freezer", blue + "Omg you cant bypas the freezer.");
            }
        }
        public void OnPlayerDisconnected(Fougerite.Player pl)
        {
            if (BanPlayerWhenLeave)
            {
                var id = pl.SteamID;
                if (Frozen.Contains(id))
                {
                    Fougerite.Server.GetServer().BanPlayer(pl, "Freezer", "Was banned for leaving while being frozen");
                    Fougerite.Server.GetServer().Broadcast(pl.Name + red + " Has been banned for leaving whole being frozen");
                }
            }
        }
        public void PlayerHurt(HurtEvent he)
        {
            if (he.AttackerIsPlayer && he.VictimIsPlayer)
            {
                Fougerite.Player attacker = (Fougerite.Player)he.Attacker;
                Fougerite.Player victim = (Fougerite.Player)he.Victim;
                var id = victim.SteamID;
                var id1 = attacker.SteamID;
                if (Frozen.Contains(id1))
                {
                    attacker.Notice("✘", "Player " + victim.Name + " Is frozen you cant kill him");
                    he.DamageAmount = 0f;
                }
                else if (Frozen.Contains(id))
                {
                    attacker.Notice("✘", "You cant kill while being frozen");
                    he.DamageAmount = 0f;
                }
            }
        }
    }
}
