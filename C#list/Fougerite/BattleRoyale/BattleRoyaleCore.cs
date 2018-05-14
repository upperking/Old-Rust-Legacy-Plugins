using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//references
using Fougerite;
using Fougerite.Events;
using UnityEngine;
using System.IO;

using RoyaleUser = Fougerite.Player;


namespace BattleRoyale
{
    public class BattleRoyaleCore : Fougerite.Module
    {
        public IniParser ini;
        public IniParser lootlist;

        DataStore ds = DataStore.GetInstance();
        Server server = Server.GetServer();
        World world = World.GetWorld();


        public string StartMessage = "BattleRoyale has been started for {0} Minute(s)";
        public string DeathMessage = "{0} has killed {1} with a {2} from {3} m";
        public string WinnerMessage = "{0} has won the battle royale with an amount of {1} kills";
        public string EndMessage = "Battle has ended";
        public string StartNotice = "BattleRoyale has been started for {0} Minute(s)";
        public string WinnerNotice = "{0} has won the match";
        public string NotEnoughPlayersMessage = "There are not enough players to start a battle";
        public string StartStartTimer = "Battle royale wil begin withtin {0} Second(s)";
        public string WaitMessage = "Next battle wil start in {0} Second(s)";
        public string JoinMessage = "{0} has joined Battle royale";
        public string LeaveMessage = "{0} has left the server";

        public int MinPlayers = 5;
        public int WaitTimer = 30;
        public int BattleTimer = 20;

        public bool BarricadePlus = true;
        public bool npcgod = true;

        public override string Name { get { return "BattleRoyale"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "BattleRoyale"; } }
        public override Version Version { get { return new Version("1.0"); } }

        private List<ulong> inroyale = new List<ulong>();
        private List<ulong> inlobby = new List<ulong>();
        public bool enabled = false;


        public override void Initialize()
        {

            LoadHooks();
            CheckConfig();
            CheckLootList();
            SendLobby();
            enabled = true;
        }
        void LoadHooks()
        {
            Logger.Log("[BattleRoyale Beta 1] Loading Hooks");
            Fougerite.Hooks.OnCommand += Command;
            Fougerite.Hooks.OnEntityDeployedWithPlacer += Deploy;
            Fougerite.Hooks.OnPlayerConnected += Connected;
            Fougerite.Hooks.OnPlayerDisconnected += Disconnect;
            Fougerite.Hooks.OnPlayerHurt += Hurt;

            Fougerite.Hooks.OnPlayerKilled += Killed;
            Fougerite.Hooks.OnPlayerSpawned += Spawned;
            Logger.Log("[BattleRoyale Beta 1] Hooks Loaded");
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnCommand -= Command;
            Fougerite.Hooks.OnEntityDeployedWithPlacer -= Deploy;
            Fougerite.Hooks.OnPlayerConnected -= Connected;
            Fougerite.Hooks.OnPlayerDisconnected -= Disconnect;
            Fougerite.Hooks.OnPlayerKilled -= Killed;
            Fougerite.Hooks.OnPlayerSpawned -= Spawned;
            Fougerite.Hooks.OnPlayerHurt -= Hurt;
        }
        void CheckConfig()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "BattleRoyale.ini")))
            {
                Logger.LogWarning("[BattleRoyale Beta 1] BattleRoyale.ini not found... creating one now");
                File.Create(Path.Combine(ModuleFolder, "BattleRoyale.ini")).Dispose();
                ini = new IniParser(Path.Combine(ModuleFolder, "BattleRoyale.ini"));
                ini.AddSetting("Options", "BattleTimer", BattleTimer.ToString());
                ini.AddSetting("Options", "npcgod", npcgod.ToString());
                ini.AddSetting("Options", "WaitTimer", WaitTimer.ToString());
                ini.AddSetting("Options", "MinPlayers", MinPlayers.ToString());
                ini.AddSetting("Options", "BarricadePlus", BarricadePlus.ToString());
                ini.AddSetting("Messages", "StartMessage", StartMessage.ToString());
                ini.AddSetting("Messages", "DeathMessage", DeathMessage.ToString());
                ini.AddSetting("Messages", "WinnerMessage", WinnerMessage.ToString());
                ini.AddSetting("Messages", "EndMessage", EndMessage.ToString());
                ini.AddSetting("Messages", "StartNotice", StartNotice.ToString());
                ini.AddSetting("Messages", "WinnerNotice", WinnerNotice.ToString());
                ini.AddSetting("Messages", "NotEnoughPlayersMessage", NotEnoughPlayersMessage.ToString());
                ini.AddSetting("Messages", "StartStartTimer", StartStartTimer.ToString());
                ini.AddSetting("Messages", "WaitMessage", WaitMessage.ToString());
                ini.AddSetting("Messages", "JoinMessage", JoinMessage.ToString());
                ini.AddSetting("Messages", "LeaveMessage", LeaveMessage.ToString());
                ini.AddSetting("Items", "Rock", "1");
                ini.AddSetting("Items", "Torch", "1");
                ini.AddSetting("Items", "Bandage", "1");       
                ini.Save();
                Logger.Log("[BattleRoyale Beta 1] File created");
            }
            else
            {
                Logger.Log("[BattleRoyale Beta 1] Reading settings file......");
                ini = new IniParser(Path.Combine(ModuleFolder, "BattleRoyale.ini"));
                BattleTimer = int.Parse(ini.GetSetting("Options", "BattleTimer"));
                npcgod = bool.Parse(ini.GetSetting("Options", "npcgod"));
                WaitTimer = int.Parse(ini.GetSetting("Options", "WaitTimer"));
                MinPlayers = int.Parse(ini.GetSetting("Options", "MinPlayers"));
                BarricadePlus = bool.Parse(ini.GetSetting("Options", "BarricadePlus"));
                StartMessage = ini.GetSetting("Messages", "StartMessage");
                DeathMessage = ini.GetSetting("Messages", "DeathMessage");
                WinnerMessage = ini.GetSetting("Messages", "WinnerMessage");
                EndMessage = ini.GetSetting("Messages", "EndMessage");
                StartNotice = ini.GetSetting("Messages", "StartNotice");
                WinnerNotice = ini.GetSetting("Messages", "WinnerNotice");
                NotEnoughPlayersMessage = ini.GetSetting("Messages", "NotEnoughPlayersMessage");
                StartStartTimer = ini.GetSetting("Messages", "StartStartTimer");
                WaitMessage = ini.GetSetting("Messages", "WaitMessage");
                JoinMessage = ini.GetSetting("Messages", "JoinMessage");
                LeaveMessage = ini.GetSetting("Messages", "LeaveMessage");
            }

        }
        void CheckLootList()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "lootlist")))
            {
                File.Create(Path.Combine(ModuleFolder, "lootlist.ini")).Dispose();
                lootlist = new IniParser(Path.Combine(ModuleFolder, "lootlist.ini"));
                lootlist.AddSetting("WeaponLootBox", "1", "(5797, 397, -3439)");
                lootlist.AddSetting("MedicalLootBox", "1", "(5797, 397, -3439)");
                lootlist.AddSetting("SupplyCrate", "1", "(5716, 406, -3400)");
                lootlist.AddSetting("BoxLoot", "1", "(5716, 406, -3400)");
                lootlist.AddSetting("AmmoLootBox", "1", "(6140, 383, -3429)");
                lootlist.Save();
            }
            else
            {
                lootlist = new IniParser(Path.Combine(ModuleFolder, "lootlist.ini"));
            }
        }
        void SendLobby()
        {
            server.BroadcastFrom("BattleRoyale", EndMessage);
            if (server.Players.Count() >= MinPlayers)
            {
                string timer = Convert.ToString(WaitTimer);
                string msg = WaitMessage.Replace("{0}", timer);
                server.BroadcastFrom("BattleRoyale", msg);
                foreach (RoyaleUser pl in server.Players)
                {
                    if (inroyale.Contains(pl.UID)) { inroyale.Remove(pl.UID); }
                    if (!inlobby.Contains(pl.UID)) { inlobby.Add(pl.UID); }
                    Vector3 pos = (Vector3)ds.Get("LobbySpawn", "spawn");
                    pl.TeleportTo(pos);                
                    waittimer(WaitTimer * 1000, null).Start();
                }
            }
            else
            {
                server.BroadcastFrom("BattleRoyale", NotEnoughPlayersMessage);
            }
        }
        void Spawned(RoyaleUser pl, SpawnEvent se)
        {
            if (!inlobby.Contains(pl.UID))
            {
                if (ds.ContainsKey("LobbySpawn", "spawn"))
                {
                    Vector3 pos = (Vector3)ds.Get("LobbySpawn", "spawn");
                    pl.TeleportTo(pos);
                    inlobby.Add(pl.UID);
                    pl.MessageFrom("BattleRoyale", "Teleported to lobby");
                }

            }
        }
        void Connected(RoyaleUser pl)
        {
            string msg = JoinMessage.Replace("{0}", pl.Name);
            server.BroadcastFrom("BattleRoyale", msg);
        }
        void Disconnect(RoyaleUser pl)
        {
            string msg = LeaveMessage.Replace("{0}", pl.Name);
            server.BroadcastFrom("BattleRoyale", msg);
        }
        void Killed(DeathEvent de)
        {
            if (de.AttackerIsPlayer && de.VictimIsPlayer)
            {
                RoyaleUser attacker = (RoyaleUser)de.Attacker;
                RoyaleUser victim = (RoyaleUser)de.Victim;
                if (inroyale.Contains(attacker.UID) && inroyale.Contains(victim.UID))
                {
                    if (attacker == victim)
                    {
                        server.BroadcastFrom("RoyalDeath", victim.Name + " Killed himself using suicide lel");
                        return;
                    }
                    string weapon = de.WeaponName;
                    float dist = Vector3.Distance(attacker.Location, victim.Location);
                    string dista = Convert.ToString(dist);
                    string msg = DeathMessage.Replace("{0}", attacker.Name).Replace("{1}", victim.Name).Replace("{2}", weapon).Replace("{3}", dista);
                    server.BroadcastFrom("RoyaleDeath", msg);
                }
            }
        }
        void Hurt(HurtEvent he)
        {
            if (he.AttackerIsNPC && he.VictimIsPlayer)
            {
                if (npcgod)
                {
                    he.DamageAmount = 0f;
                }
                return;
            }
            RoyaleUser attacker = (RoyaleUser)he.Attacker;
            RoyaleUser victim = (RoyaleUser)he.Victim;
            if (he.AttackerIsPlayer && he.VictimIsPlayer)
            {
                if (inlobby.Contains(attacker.UID) && inlobby.Contains(victim.UID))
                {
                    he.DamageAmount = 0f;
                    return;
                }
            }
        }
        void Deploy(RoyaleUser pl, Fougerite.Entity ent, RoyaleUser actualplacer)
        {
            if (ent != null)
            {
                if (BarricadePlus)
                {
                    if (ent.Name == "Wood Barricade")
                    {
                        ds.Add("barloc", "loc", ent.Location);
                        Vector3 loc = (Vector3)ds.Get("barloc", "loc");
                        ent.Destroy();
                        world.Spawn(";struct_wood_wall", loc);
                    }
                }
            }
            else if (inlobby.Contains(actualplacer.UID))
            {
                ent.Destroy();
            }
        }
        void Command(RoyaleUser pl, string cmd, string[] args)
        {
            if (cmd == "battleroyale")
            {
                pl.MessageFrom("BattleRoyale", "[color #bf3eff]************************************************************************************************************");
                pl.MessageFrom("BattleRoyale", "[color #bcee68] BATTLE ROYALE 1 BETA by ice cold ");
                pl.MessageFrom("BattleRoyale", "[color #b4eeb4]1: You spawn on the spawn point of the initial playable area with a torch, bandage and map at sunrise. Find loot as you make your way to the center of the map.");
                pl.MessageFrom("BattleRoyale", "[color #b4eeb4]2: Loot spawns on random places around the map. Supply drops contain rare loot.");
                pl.MessageFrom("BattleRoyale", "[color #b4eeb4]3: You are surviving. Continue moving across the map and remember do not get killed");
                pl.MessageFrom("BattleRoyale", "[color #bf3eff]************************************************************************************************************");
                if (pl.Admin)
                {
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/battle_restart = restarts the plugin and teleports all the players to lobby");
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/battle_on = enables plugin");
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/battle_addlobby = adds the spawn for lobby (required)");
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/battle_addspawn = adds the spawn for battle (required)");
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/spawn_box1 = adds location for weaponbox");
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/spawn_box2 = adds location for medicalbox");
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/spawn_box3 = adds location for junkbox");
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/spawn_box4 = adds location for SupplyCrate");
                    pl.MessageFrom("BattleRoyale", "[color #a4d3ee]/spawn_box5 = adds location for AmmoBox");
                }
            }
            else if (cmd == "battle_restarts")
            {
                if (pl.Admin)
                {
                    foreach (RoyaleUser player in server.Players)
                    {
                        if (ds.ContainsKey("LobbySpawn", "spawn"))
                        {
                            Vector3 lobby = (Vector3)ds.Get("LobbySpawn", "spawn");
                            inroyale.Remove(player.UID);
                            inlobby.Add(player.UID);
                            player.TeleportTo(lobby);
                            //testing if kit works 
                          //  GiveLoadout(pl);
                        }
                        else
                        {
                            pl.MessageFrom("ERROR", "[color red]Please make a lobby");
                        }
                    }
                }
            }
            else if (cmd == "battle_addlobby")
            {
                if (pl.Admin)
                {
                    ds.Add("LobbySpawn", "spawn", pl.Location);
                    ds.Save();
                    pl.MessageFrom("BattleRoyale", "Lobby spawn has been added");
                }
            }
            else if (cmd == "battle_addspawn")
            {
                if (pl.Admin)
                {
                    ds.Add("BattleSpawn", "spawn", pl.Location);
                    ds.Save();
                    pl.MessageFrom("BattleRoyale", "Master spawn has been added");
                }
            }
            else if (cmd == "spawn_box1")
            {
                if (pl.Admin)
                {
                    string[] c = lootlist.EnumSection("WeaponLootBox");
                    string co = (Convert.ToInt32(c[c.Length - 1]) + 1).ToString();
                    lootlist.AddSetting("WeaponLootBox", co, pl.Location.ToString());
                    lootlist.Save();
                    pl.MessageFrom("BattleRoyale", "Added");
                }

            }
            else if (cmd == "spawn_box2")
            {
                if (pl.Admin)
                {
                    string[] c = lootlist.EnumSection("MedicalLootBox");
                    string co = (Convert.ToInt32(c[c.Length - 1]) + 1).ToString();
                    lootlist.AddSetting("MedicalLootBox", co.ToString(), pl.Location.ToString());
                    lootlist.Save();
                    pl.MessageFrom("BattleRoyale", "Added");
                }

            }
            else if (cmd == "spawn_box3")
            {
                if (pl.Admin)
                {
                    string[] c = lootlist.EnumSection("SupplyCrate");
                    string co = (Convert.ToInt32(c[c.Length - 1]) + 1).ToString();
                    lootlist.AddSetting("SupplyCrate", co.ToString(), pl.Location.ToString());
                    lootlist.Save();
                    pl.MessageFrom("BattleRoyale", "Added");
                }
            }
            else if (cmd == "spawn_box4")
            {
                if (pl.Admin)
                {
                    string[] c = lootlist.EnumSection("BoxLoot");
                    string co = (Convert.ToInt32(c[c.Length - 1]) + 1).ToString();
                    lootlist.AddSetting("BoxLoot", co.ToString(), pl.Location.ToString());
                    lootlist.Save();
                    pl.MessageFrom("BattleRoyale", "Added");
                }
            }
            else if (cmd == "spawn_box5")
            {
                if (pl.Admin)
                {
                    string[] c = lootlist.EnumSection("AmmoLootBox");
                    string co = (Convert.ToInt32(c[c.Length - 1]) + 1).ToString();
                    lootlist.AddSetting("AmmoLootBox", co.ToString(), pl.Location.ToString());
                    lootlist.Save();
                    pl.MessageFrom("BattleRoyale", "Added");
                }
            }
        }
        void GiveLoadout(RoyaleUser pl)
        {
            string[] en = ini.EnumSection("Items");
            foreach(var item in en)
            {                  
                int hi = Convert.ToInt32(ini.GetSetting("Items", item));
                int amount = hi;
                pl.Inventory.AddItem(item, amount);
            }

        }
        public TimedEvent waittimer(int timeoutDelay, Dictionary<string, object> args)
        {
            TimedEvent timedEvent = new TimedEvent(timeoutDelay);
            timedEvent.Args = args;
            timedEvent.OnFire += CallBack;
            return timedEvent;
        }
        public void CallBack(TimedEvent e)
        {
            var dict = e.Args;
            e.Kill();
            foreach(RoyaleUser pl in server.Players)
            {
                if (inlobby.Contains(pl.UID))
                {
                    pl.Inventory.ClearAll();
                    Vector3 pos = (Vector3)ds.Get("BattleSpawn", "spawn");
                    GiveLoadout(pl);
                    pl.TeleportTo(pos);
                    string time = Convert.ToString(WaitTimer);
                    string message = StartMessage.Replace("{0}", time);
                    string notice = StartNotice.Replace("{0}", time);
                    pl.MessageFrom("battleRoyale", message);
                    inlobby.Remove(pl.UID);
                    inroyale.Add(pl.UID);
                    pl.Notice(notice);                
                }             
            }
            SpawnLoot();
            battletimer(BattleTimer * 60000, null).Start();
        }
        public TimedEvent battletimer(int timeoutDelay, Dictionary<string, object> args)
        {
            TimedEvent timedEvent = new TimedEvent(timeoutDelay);
            timedEvent.Args = args;
            timedEvent.OnFire += CallBack1;
            return timedEvent;
        }
        public void CallBack1(TimedEvent e)
        {
            var dict = e.Args;
            e.Kill();
            foreach (RoyaleUser pl in server.Players)
            {
                if (inroyale.Contains(pl.UID))
                {
                    Vector3 pos = (Vector3)ds.Get("LobbySpawn", "spawn");
                    pl.MessageFrom("BattleRoyale", EndMessage);
                    inroyale.Remove(pl.UID);
                    inlobby.Add(pl.UID);
                    pl.Inventory.ClearAll();
                    pl.TeleportTo(pos);
                }
            }
            StartWaitTimer();
            
        }
        void StartWaitTimer()
        {
            if(server.Players.Count() >= MinPlayers)
            {
                string timer = Convert.ToString(WaitTimer);
                string msg = WaitMessage.Replace("{0}", timer);
                server.BroadcastFrom("BattleRoyale", msg);
                waittimer(WaitTimer * 1000, null).Start();
            }        
        }
    }
}
