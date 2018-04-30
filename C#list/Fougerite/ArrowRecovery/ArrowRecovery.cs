using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Fougerite;
using Fougerite.Events;

namespace ArrowRecovery
{
    public class ArrowRecovery : Fougerite.Module
    {
        public IniParser ini;
        public int BreakChange = 50;
        public bool AnnounceData;

        public static System.Random rnd;

        public override string Name { get { return "ArrowRecovery"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "the C# version from the javascripted one by Doctor"; } }
        public override Version Version { get { return new Version("1.0"); } }
        public override void Initialize()
        {
            rnd = new System.Random();
            if (!File.Exists(Path.Combine(ModuleFolder, "Settings.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Settings.ini")).Dispose();
                ini = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                ini.AddSetting("ArrowRecovery", "BreakChange", BreakChange.ToString());
                ini.AddSetting("ArrowRecovery", "AnnounceData", "false");
                ini.Save();
            }
            else
            {
                ini = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                BreakChange = int.Parse(ini.GetSetting("ArrowRecovery", "BreakChange"));
                AnnounceData = bool.Parse(ini.GetSetting("ArrowRecovery", "AnnounceData"));
            }
            Fougerite.Hooks.OnPlayerKilled += PlayerKilled;
            Fougerite.Hooks.OnNPCKilled += NPCKilled;
            Fougerite.Hooks.OnCommand += Command;
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnPlayerKilled -= PlayerKilled;
            Fougerite.Hooks.OnNPCKilled -= NPCKilled;
            Fougerite.Hooks.OnCommand -= Command;
        }
        public void PlayerKilled(DeathEvent de)
        {
            Player attacker = (Player)de.Attacker;
            Player victim = (Player)de.Victim;
            string weapon = de.WeaponName;          
            if (de.AttackerIsPlayer && de.VictimIsPlayer)
            {
                if (de.Attacker != de.Victim)
                {                 
                    if (weapon == "Hunting Bow")
                    {                       
                        int d = rnd.Next(100);
                        if (d < BreakChange)
                        {
                            if (AnnounceData)
                            {
                                attacker.Message("Arrow broke when it hit the target! :(");
                            }
                        }
                        else
                        {
                            var Arrows = GetArrows(attacker) + 1;
                            if (AnnounceData)
                            {
                                attacker.Message("Arrow did not break when it hit the target! :D");
                                attacker.Message(Arrows + " are in the body");
                            }
                            DataStore.GetInstance().Add("ArrowRecovery", attacker.SteamID, Arrows);
                        }
                    }
                }
                // please let this one lowecase otherwise it wil trows error at line line 71
                var arrows = GetArrows(attacker);
                if (arrows > 0)
                {
                    attacker.Notice("You have recoved " + arrows + " arrows from the corpse");
                    attacker.Inventory.AddItem("Arrow", arrows);
                    DataStore.GetInstance().Add("ArrowRecovery", attacker.SteamID + "arrow", "0");
                }
            }
        }      
        public void NPCKilled(DeathEvent de)
        {
            Player attacker = (Player)de.Attacker;
            string weapon = de.WeaponName;
            if (de.VictimIsNPC)
            {
                if (de.Victim != de.Attacker)
                {
                    if (weapon == "Hunting Bow")
                    {
                        int d = rnd.Next(100);
                        if (d < BreakChange)
                        {
                            if (AnnounceData)
                            {
                                attacker.Message("Arrow broke when it hit the target! :(");
                            }
                        }
                        else
                        {
                            var Arrows = GetArrows(attacker) + 1;
                            if (AnnounceData)
                            {
                                attacker.Message("Arrow did not break when it hit the target! :D");
                                attacker.Message(Arrows + " are in the body");
                            }
                        }
                    }
                }
                var arrows = GetArrows(attacker);
                if (arrows > 0)
                {
                    attacker.Notice("You have recoved " + arrows + " arrows from the corpse");
                    attacker.Inventory.AddItem("Arrow", arrows);
                    DataStore.GetInstance().Add("ArrowRecovery", attacker.SteamID + "arrow", "0");
                }
            }
        }
        public int GetArrows(Fougerite.Player pl)
        {
            var arrow = int.TryParse(DataStore.GetInstance().Get("ArrowRecovery", pl.SteamID));
            if (arrow == null)
            {
                arrow = 0;
            }
            return arrow;
        }
    }
}
