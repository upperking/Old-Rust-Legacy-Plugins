using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Fougerite;
using Fougerite.Events;
using UnityEngine;

namespace AntiLongdistloot
{
    public class DetectionClass : Fougerite.Module
    {
        private IniParser ini;
        public bool BroadcastAdmins;
        public float MaxlootDist = 10f;
        private string yellow = "[color yellow]";
        public string Broadcast = "{Player} has been kicked for using autoloot exploit/glitch";
        public string WarningMessage = "You cannot autoloot on this server";


        public override string Name { get { return "AntiCrackLoot"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Blocks the long distance loot exploit/glitch"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public static string ppath;
        public static System.IO.StreamWriter file;
        public string pathfile = Directory.GetCurrentDirectory() + "\\save\\AntiCrackLoot\\Detections.log";

        public override void Initialize()
        {
            Fougerite.Hooks.OnItemRemoved += ItemRemoved;

            ppath = Path.Combine(ModuleFolder, "Detections.log");

            if (!File.Exists(Path.Combine(ModuleFolder, "Config")))
            {
                File.Create(Path.Combine(ModuleFolder, "Config.ini")).Dispose();
                ini = new IniParser(Path.Combine(ModuleFolder, "Config.ini"));
                ini.AddSetting("Options", "BroadcastAdmins", "true");
                ini.AddSetting("Messages", "Broadcast", Broadcast.ToString());
                ini.AddSetting("Messages", "WarningMessage", WarningMessage.ToString());
                ini.Save();
                Logger.Log("[AntiAutoloot] Config not found and created");
            }
            else
            {
                Logger.Log("[AntiAutoloot] Config file loaded");
                ini = new IniParser(Path.Combine(ModuleFolder, "Config.ini"));
                BroadcastAdmins = bool.Parse(ini.GetSetting("Options", "BroadcastAdmins"));
                Broadcast = ini.GetSetting("Messages", "Broadcast");
                WarningMessage = ini.GetSetting("Messages", "WarningMessage");
            }
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnItemRemoved -= ItemRemoved;
        }
        public void ItemRemoved(InventoryModEvent e)
        {
            try
            {
                if (e.Player != null)
                {
                    Fougerite.Player player = (Fougerite.Player)e.Player;
                    if (player.IsOnline)
                    {
                        Vector3 location = player.Location;
                        float d = Vector3.Distance(e.Inventory.transform.position, location);
                        if (d > MaxlootDist)
                        {
                            if (BroadcastAdmins)
                            {
                                NotifyAdmin(player.Name + yellow + " Is Maybe using longdistance looting dist: " + (Math.Round(d)));
                            }
                            string line = DateTime.Now + " [AntiAutoLoot] " + player.Name + "-" + player.SteamID + " Tried to loot from " + (Math.Round(d)) + " max dist = " + MaxlootDist + "";
                            file = new System.IO.StreamWriter(ppath, true);
                            file.WriteLine(line);
                            file.Close();
                            string message = Broadcast.Replace("{Player}", player.Name);
                            Server.GetServer().Broadcast(message);
                            player.Message(WarningMessage);
                            Logger.LogWarning(player.Name + " Tried to loot from an unreachable container");
                            e.Cancel();
                            player.Disconnect();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[AntiCrackLoot] ERROR: " + ex);
            }
        }
        private static void NotifyAdmin(string message)
        {
            foreach (Fougerite.Player pl in Fougerite.Server.GetServer().Players.Where(pl => pl.Admin || pl.Moderator))
            {
                pl.MessageFrom("AntiAutoLoot", message);
            }
        }
    }
}