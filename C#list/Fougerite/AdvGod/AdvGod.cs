using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.Events;
using System.IO;

namespace AdvGod
{
    public class AdvGod : Fougerite.Module
    {
        private string green = "[color #82FA58]";
        private string yellow = "[color #F4FA58]";
        private string red = "[color #FF0000]";
        public static string ppath;
        public static System.IO.StreamWriter file;
        public string pathfile = Directory.GetCurrentDirectory() + "\\save\\AdvGod\\Commands.log";
        public override string Name { get { return "AdvGod"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Allow admins to give god to other players and also admins can give god to themselves"; } }
        public override Version Version { get { return new Version("1.1"); } }

        #region Initialize
        public override void Initialize()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "Commands.log"))) { File.Create(Path.Combine(ModuleFolder, "Commands.log")).Dispose(); }
            {
                ppath = Path.Combine(ModuleFolder, "Commands.log");
            }
            Fougerite.Hooks.OnCommand += Cmd;
            Fougerite.Hooks.OnPlayerHurt += OnPlayerHurt;
            Fougerite.Hooks.OnEntityHurt += EntHurt;
            Fougerite.Hooks.OnPlayerDisconnected += OnNetUserDisconnected;
            Logger.Log("AdvGod System loaded");
        }
        #endregion
        #region DeInitialized
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnCommand -= Cmd;
            Fougerite.Hooks.OnPlayerHurt -= OnPlayerHurt;
            Fougerite.Hooks.OnEntityHurt -= EntHurt;
            Fougerite.Hooks.OnPlayerDisconnected -= OnNetUserDisconnected;
            Logger.Log("AdvGod unloaded");
        }
        #endregion
        #region AdvGodCore
        public void Cmd(Fougerite.Player netuser, string cmd, string[] args)
        {
            if (cmd == "god")
            {
                if (netuser.Admin)
                {
                    netuser.Notice("☤", "AdvGod by ice cold", 15f);
                    netuser.MessageFrom(Name, yellow + "/godon >= Turn god on");
                    netuser.MessageFrom(Name, yellow + "/godoff >= Turn god off");
                    netuser.MessageFrom(Name, yellow + "/givegod <Name> >= Gives god to specific player");
                    netuser.MessageFrom(Name, yellow + "/remgod <Name> >= Remove god from specific player");
                    netuser.MessageFrom(Name, green + "Check all my public work on " + yellow + " https://github.com/icecolderino");
                }
                else
                {
                    netuser.MessageFrom(Name, red + "You dont have permissions to use /god");
                }
            }
            else if (cmd == "godon")
            {
                if (netuser.Admin)
                {                 
                    if (!DataStore.GetInstance().ContainsKey("AdvgodOn", netuser.SteamID))
                    {                    
                        var id = netuser.SteamID;
                        netuser.MessageFrom(Name, green + "Godmode enabled");
                        Server.GetServer().BroadcastFrom(Name, green + netuser.Name + yellow + " Is now now in godmode");
                        netuser.Notice("☤", "Godmode enabled");
                        DataStore.GetInstance().Add("AdvgodOn", id, "on");
                        string line = DateTime.Now + " " + netuser.Name + ": Has used the /godon command";
                        file = new System.IO.StreamWriter(ppath, true);
                        file.WriteLine(line);
                        file.Close();
                    }
                    else
                    {
                        netuser.MessageFrom(Name, yellow + "You already have godmode enabled type /godoff to disable it");
                    }
                }
                else
                {
                    netuser.MessageFrom(Name, red + "You dont have permissions to use /godon");
                }
            }
            else if(cmd == "godoff")
            {
                if(!netuser.Admin) { netuser.MessageFrom(Name, red + "You dont have permissions to use /godoff"); return; }
                if(DataStore.GetInstance().ContainsKey("AdvgodOn", netuser.SteamID))
                {
                    var id = netuser.SteamID;
                    netuser.MessageFrom(Name, "Godmode disabled");
                    Server.GetServer().BroadcastFrom(Name, green + netuser.Name + yellow + "Has disabled godmode");
                    netuser.Notice("☤", "Godmode disabled");
                    DataStore.GetInstance().Remove("AdvgodOn", id);
                    string line = DateTime.Now + " " + netuser.Name + ": Has used the /godon command";
                    file = new System.IO.StreamWriter(ppath, true);
                    file.WriteLine(line);
                    file.Close();
                }           
            }
            else if (cmd == "givegod")
            {
                if (netuser.Admin)
                {
                    string s = string.Join(" ", args);
                    Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                    if (p == null)
                    {
                        netuser.MessageFrom(Name, yellow + "Failed to find " + red + s);
                    }
                    else if (args.Length.Equals(0))
                    {
                        netuser.MessageFrom(Name, yellow + "Wrong Syntax use /givegod <name>");
                    }
                    else
                    {
                        Fougerite.Player targetuser = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                        if (!targetuser.Equals(null))
                        {
                            var nname = netuser.Name;
                            var tname = targetuser.Name;
                            targetuser.MessageFrom(Name, "You have received godmode powers by " + nname);
                            netuser.MessageFrom(Name, "You have given godmode to " + tname);
                            netuser.Notice("☤", "You have given godmode to " + tname);
                            targetuser.Notice("☤", nname + " has given godmoe to you");
                            DataStore.GetInstance().Add("hasadvgod", targetuser.SteamID, "on");
                            string line = DateTime.Now + " " + nname + ": Has used the /givegod command";
                            file = new System.IO.StreamWriter(ppath, true);
                            file.WriteLine(line);
                            file.Close();
                        }
                    }
                }
            }
            else if (cmd == "remgod")
            {
                if (netuser.Admin)
                {
                    string s = string.Join(" ", args);
                    Fougerite.Player p = Fougerite.Server.GetServer().FindPlayer(s);
                    if (p == null)
                    {
                        netuser.MessageFrom(Name, yellow + "Failed to find " + red + s);
                    }
                    else if (args.Length.Equals(0))
                    {
                        netuser.MessageFrom(Name, yellow + "Wrong Syntax use /remgod <name>");
                    }
                    else
                    {
                        Fougerite.Player targetuser = Fougerite.Server.GetServer().FindPlayer(string.Join(" ", args)) ?? null;
                        if (!targetuser.Equals(null))
                        {
                            var nname = netuser.Name;
                            var tname = targetuser.Name;
                            targetuser.MessageFrom(Name, "Your godly powers are removed by " + nname);
                            netuser.MessageFrom(Name, "You have removed godmode from " + tname);
                            netuser.Notice("☤", "You have removed godmode from " + tname);
                            targetuser.Notice("☤", nname + " has removed your godmode");
                            DataStore.GetInstance().Remove("hasadvgod", targetuser.SteamID);
                            string line = DateTime.Now + " " + nname + ": Has used the /remgod command";
                            file = new System.IO.StreamWriter(ppath, true);
                            file.WriteLine(line);
                            file.Close();
                        }
                    }
                }
            }
        }
        public void OnNetUserDisconnected(Fougerite.Player netuser)
        {
            if (DataStore.GetInstance().ContainsKey("hasadvgod", netuser.SteamID) || (DataStore.GetInstance().ContainsKey("AdvgodOn", netuser.SteamID)))
            {
                DataStore.GetInstance().Remove("hasadvgod", netuser.SteamID);
                DataStore.GetInstance().Remove("AdvgodOn", netuser.SteamID);
            }
        }
        public void OnPlayerHurt(HurtEvent he)
        {
            if (he.AttackerIsPlayer && he.VictimIsPlayer)
            {
                Fougerite.Player attacker = (Fougerite.Player)he.Attacker;
                Fougerite.Player victim = (Fougerite.Player)he.Victim;
                if (DataStore.GetInstance().ContainsKey("hasadvgod", victim.SteamID) || DataStore.GetInstance().ContainsKey("AdvgodOn", victim.SteamID))
                {
                    attacker.Notice("✘", victim.Name + " is currently in godmode this means you cant kill him");
                    he.DamageAmount = 0f;
                }
                else if (DataStore.GetInstance().ContainsKey("hasadvgod", attacker.SteamID) || DataStore.GetInstance().ContainsKey("AdvgodOn", attacker.SteamID))
                {
                    attacker.Notice("✘", "You cannot hurt people while in godmode");
                    he.DamageAmount = 0f;
                }
            }
        }
        public void EntHurt(HurtEvent he)
        {
            Fougerite.Player attacker = (Fougerite.Player)he.Attacker;
            if (he.Attacker != null && (!he.IsDecay))
            {
                if (DataStore.GetInstance().ContainsKey("hasadvgod", attacker.SteamID) || (DataStore.GetInstance().ContainsKey("AdvgodOn", attacker.SteamID)))
                {
                    attacker.Notice("✘", "You cannot damage structures when in godmode");
                    he.DamageAmount = 0f;
                }
            }
            else
            {
                //Logger.LogWarning("Attacker was null while entity hurt or damage was decay");
            }
        }
    }
}
#endregion
