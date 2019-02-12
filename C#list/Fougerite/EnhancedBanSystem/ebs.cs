using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using System.IO;
using Fougerite.Events;
using uLink;
using Newtonsoft.Json;
using System.Diagnostics;


namespace EnhancedBanSystem
{
    public class ebs : Module
    {
        public Config config;
        public override string Name => "EnhancedBanSystem";
        public override string Author => "ice cold";
        public override string Description => "EnhancedBanSystem";
        public override Version Version => new Version("1.0");
        public IniParser banlist;

        public Dictionary<string, TimerPlus> minutebans;
        public Dictionary<string, TimerPlus> hourbans;
        public Dictionary<string, TimerPlus> daybans;

        public override void Initialize()
        {      
         //   Hooks.OnServerShutdown += OnShutDown;
            Hooks.OnPlayerApproval += OnPlayerApproval;
            Hooks.OnCommand += OnCommand;
            Hooks.OnServerLoaded += OnServerLoaded;
            Hooks.OnServerShutdown += OnServerShutDown;

            CheckBanlist();
            ReadConfig();

       
           

        }

        private void CheckBanlist()
        {
            if(!File.Exists(Path.Combine(ModuleFolder, "Banlist.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Banlist.ini")).Dispose();
                banlist = new IniParser(Path.Combine(ModuleFolder, "Banlist.ini"));
                banlist.Save();
            }
            else
            {
                banlist = new IniParser(Path.Combine(ModuleFolder, "Banlist.ini"));
            }
        }

        private void OnServerLoaded()
        {
            minutebans = new Dictionary<string, TimerPlus>();
            hourbans = new Dictionary<string, TimerPlus>();
            daybans = new Dictionary<string, TimerPlus>();
            LoadBanlist();
        }

        private void OnCommand(Fougerite.Player player, string cmd, string[] args)
        {
            if(cmd == "ebsban")
            {
                if(player.Admin || player.Moderator)
                {
                    if(args.Length == 4)
                    {
                        Logger.Log("1");
                        Fougerite.Player target = Server.GetServer().FindPlayer(args[0]);
                        Logger.Log("2");
                        if (target == null) { player.Notice("Couldn't find the target user"); return; }
                        Logger.Log("3");
                        int time = int.Parse(args[1]);
                        Logger.Log("4");
                        string letter = args[2].ToString();
                        Logger.Log("5");
                        string reason = args[3].ToString();
                        Logger.Log("6");

                        switch (letter)
                        {
                            case "m":
                                BanForMinutes(target, player, time, reason);
                                break;
                            case "h":
                                BanForHours(target, player, time, reason);
                                break;
                            case "d":
                                BanForDays(target, player, time, reason);
                                break;
                            default:
                                player.Notice("Syntax: /ebsban playername, time, m/h/d, reason");
                                break;

                        }



                    }
                }
            }
        }

        private void BanForDays(Fougerite.Player target, Fougerite.Player player, int time, string reason)
        {
            daybans[target.IP] = new TimerPlus();
            daybans[target.IP].Interval = time * 86400000;
            daybans[target.IP].AutoReset = false;
            daybans[target.IP].Elapsed += (x, y) =>
            {
                banlist.DeleteSetting("DayBans", target.IP);
                banlist.Save();
                daybans.Remove(target.IP);
            };
            daybans[target.IP].Start();

            if((bool)config.Settings["BroadcastBans"])
            {
                Server.GetServer().BroadcastNotice(config.Settings["TimedBan"].ToString().Replace("<player>", target.Name).Replace("<time>", time + " Days").Replace("<admin>", player.Name));

            }
            target.PlayerClient.netUser.Kick(NetError.ApprovalDenied, true);
        }

        private void BanForHours(Fougerite.Player target, Fougerite.Player player, int time, string reason)
        {
            hourbans[target.IP] = new TimerPlus();
            hourbans[target.IP].Interval = time * 3600000;
            hourbans[target.IP].AutoReset = false;
            hourbans[target.IP].Elapsed += (x, y) =>
            {
                banlist.DeleteSetting("HourBans", target.IP);
                banlist.Save();
                hourbans.Remove(target.IP);
            };
            hourbans[target.IP].Start();


            if ((bool)config.Settings["BroadcastBans"])
            {
                Server.GetServer().BroadcastNotice(config.Settings["TimedBan"].ToString().Replace("<player>", target.Name).Replace("<time>", time + " Hours").Replace("<admin>", player.Name));

            }
            target.PlayerClient.netUser.Kick(NetError.ApprovalDenied, true);
        }

        private void BanForMinutes(Fougerite.Player target, Fougerite.Player player, int time, string reason)
        {
            minutebans[target.IP] = new TimerPlus();
            minutebans[target.IP].Interval = time * 60000;
            minutebans[target.IP].AutoReset = false;
            minutebans[target.IP].Elapsed += (x, y) =>
            {
                banlist.DeleteSetting("MinuteBans", target.IP);
                banlist.Save();
                minutebans.Remove(target.IP);
            };
            minutebans[target.IP].Start();


            if ((bool)config.Settings["BroadcastBans"])
            {
                Server.GetServer().BroadcastNotice(config.Settings["TimedBan"].ToString().Replace("<player>", target.Name).Replace("<time>", time + " Minutes").Replace("<admin>", player.Name));

            }
            target.PlayerClient.netUser.Kick(NetError.ApprovalDenied, true);
        }

        private void ReadConfig()
        {
            if(!File.Exists(Path.Combine(ModuleFolder, "cfg_EnhancedBanSystem.json")))
            {
                File.Create(Path.Combine(ModuleFolder, "cfg_EnhancedBanSystem.json")).Dispose();
                Config settings = new Config
                {                  
                    Settings = new Dictionary<string, object>
                    {
                       {"BroadcastBans", true },
                       {"TimedBan", "<player> has been banned for <time> by <admin>" },
                       {"PermanentlyBan", "<Player> has been permanently banned for: <reason>" },

                    }                                                      
                };
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                StreamWriter file = File.AppendText(Path.Combine(ModuleFolder, "cfg_EnhancedBanSystem.json"));
                file.WriteLine(json);
                file.Close();
           
            }
            else
            {
                string json = File.ReadAllText(Path.Combine(ModuleFolder, "cfg_EnhancedBanSystem.json"));
                config = JsonConvert.DeserializeObject<Config>(json);

                Logger.Log(config.Settings["TimedBan"].ToString());
                Logger.Log(config.Settings["PermanentlyBan"].ToString());

            }
        }

        private void OnPlayerApproval(PlayerApprovalEvent e)
        {
            string ip = e.NetworkPlayerApproval.ipAddress;
            if(minutebans.ContainsKey(ip) || hourbans.ContainsKey(ip) || daybans.ContainsKey(ip))
            {
                e.ClientConnection.DenyAccess(e.NetworkPlayerApproval, "tried to join but is banned", NetError.ApprovalDenied);
            }
        }

        public override void DeInitialize()
        {
            Hooks.OnPlayerApproval -= OnPlayerApproval;
            Hooks.OnCommand -= OnCommand;
            Hooks.OnServerLoaded -= OnServerLoaded;
            Hooks.OnServerShutdown -= OnServerShutDown;
        }
        public void LoadBanlist()
        {
            try
            {
                Logger.Log("[EnhancedBanSystem-Data] Loading bans from data file...");

                Stopwatch sw = new Stopwatch();
                sw.Start();

                int count = 0;
                int globalcount = 0;
                foreach (var x in banlist.EnumSection("MinuteBans"))
                {
                    int bantime = int.Parse(banlist.GetSetting("MinuteBans", x)) * 60000;
                    StartMinuteTimer(x, bantime);
                    count++;
                    globalcount++;
                }
                Logger.Log(string.Format("[EnhancedBanSystem-Data] Succesfully loaded {0}  minute bans", count.ToString()));
                count = 0;
                foreach (var x in banlist.EnumSection("DayBans"))
                {
                    int bantime = int.Parse(banlist.GetSetting("DayBans", x)) * 86400000;
                    StartDayTimer(x, bantime);
                    count++;
                    globalcount++;
                }
                Logger.Log(string.Format("[EnhancedBanSystem-Data] Succesfully loaded {0} hour bans", count.ToString()));
                count = 0;
                foreach (var x in banlist.EnumSection("HourBans"))
                {
                    int bantime = int.Parse(banlist.GetSetting("HourBans", x)) * 3600000;
                    StartHourTimer(x, bantime);
                    count++;
                    globalcount++;
                }
                Logger.Log(string.Format("[EnhancedBanSystem-Data] Succesfully loaded {0} day bans", count.ToString()));
                count = 0;
                sw.Stop();
                Logger.Log(string.Format("[EnhancedBanSystem-StopWatch] We loaded {0} banned users in: {1} ", globalcount.ToString(), (Math.Round(sw.Elapsed.TotalSeconds))));
            }
            catch(Exception ex)
            {
                Logger.LogError("[EnhancedBanSystem-Error] Oops we caught a error while loading ban data. Report this error to the author. Error: " + ex.Message);
            }
          
        }

        private void StartHourTimer(string ip, int interval)
        {
            hourbans[ip] = new TimerPlus();
            hourbans[ip].Interval = interval;
            hourbans[ip].AutoReset = false;
            hourbans[ip].Start();
            hourbans[ip].Elapsed += (x, y) =>
            {
                banlist.DeleteSetting("HourBans", ip);
                banlist.Save();
                hourbans.Remove(ip);
            };
        }

        private void StartDayTimer(string ip, int interval)
        {
            daybans[ip] = new TimerPlus();
            daybans[ip].Interval = interval;
            daybans[ip].AutoReset = false;
            daybans[ip].Start();
            daybans[ip].Elapsed += (x, y) =>
            {
                banlist.DeleteSetting("DayBans", ip);
                banlist.Save();
                daybans.Remove(ip);
            };
        }

        public void StartMinuteTimer(string ip, int interval)
        {
            minutebans[ip] = new TimerPlus();
            minutebans[ip].Interval = interval;
            minutebans[ip].AutoReset = false;
            minutebans[ip].Start();
            minutebans[ip].Elapsed += (x, y) =>
            {
                banlist.DeleteSetting("MinuteBans", ip);
                banlist.Save();
                minutebans.Remove(ip);
            };
        }
        private void OnServerShutDown()
        {
            try
            {
                Logger.Log("[EnhancedBanSystem] Saving ban data to ini...");
                foreach (KeyValuePair<string, TimerPlus> pair in minutebans)
                {
                    banlist.AddSetting("MinuteBans", pair.Key, (Math.Round(pair.Value.TimeLeft / 60000)).ToString());
                    banlist.Save();
                }
                minutebans.Clear();
                Logger.Log("[EnhancedBanSystem] Saved minute bans");
                foreach (KeyValuePair<string, TimerPlus> pair in hourbans)
                {
                    banlist.AddSetting("HourBans", pair.Key, (Math.Round(pair.Value.TimeLeft / 3600000)).ToString());
                    banlist.Save();
                }
                hourbans.Clear();
                Logger.Log("[EnhancedBanSystem] Saved hour bans");
                foreach (KeyValuePair<string, TimerPlus> pair in daybans)
                {
                    banlist.AddSetting("DayBans", pair.Key, (Math.Round(pair.Value.TimeLeft / 86400000)).ToString());
                    banlist.Save();
                }
                daybans.Clear();
                Logger.Log("[EnhancedBanSystem] Saved day bans");
            }
            catch(Exception ex)
            {
                Logger.LogError("[EnhancedBanSystem-Error] Oops we caught a error while trying to save ban data to ini. Report this error to the author. Error: " + ex.Message);
            }
          
        }
    }
    public class Config
    {
        public Dictionary<string, object> Settings { get; set; }

    }

}
