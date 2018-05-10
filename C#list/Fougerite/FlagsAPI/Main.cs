using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Flag = Fougerite.Module;
using Flaguser = Fougerite.Player;
using System.IO;

namespace FlagsAPI
{
    public class Main : Flag
    {
        public IniParser ini;

        public override string Name { get { return "FlagsAPI"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Give flags for diffrent commands"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public override void Initialize()
        {
            Fougerite.Hooks.OnCommand += Command;

            if(!File.Exists(Path.Combine(ModuleFolder, "PlayerFlagdb.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "PlayerFlagdb.ini")).Dispose();
                ini = new IniParser(Path.Combine(ModuleFolder, "PlayerFlagdb.ini"));
                ini.Save();
            }
            else
            {
                ini = new IniParser(Path.Combine(ModuleFolder, "PlayerFlagdb.ini"));
            }       
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnCommand -= Command;
        }
        void Command(Flaguser pl, string cmd, string[] args)
        {
            if (cmd == "flag")
            {
                if (pl.Admin || Hasflag(pl, "canflag"))
                {
                    pl.Notice("⚑", "FlagsAPI brought by ice cold", 20f);
                    pl.MessageFrom(Name, "FlagsAPI 1.0 by ice cold");
                    pl.MessageFrom(Name, "/flag_add Player flagname - gives the player the flagname");
                    pl.MessageFrom(Name, "/flag_invoke Player flagname - removes the flag in player");
                }
                else
                {
                    pl.MessageFrom(Name, "You dont have the > canflag < flag");
                }
            }
            else if (cmd == "flag_add")
            {
                if(pl.Admin || Hasflag(pl, "canflag"))
                {
                    if (args.Length != 2)
                    {
                        pl.Notice("⚑", "Syntax /flag_add PlayerName FlagName");
                        return;
                    }
                    var user = Fougerite.Server.GetServer().FindPlayer(args[0]);
                    if (user == null) { pl.Notice("⚑", "Failed to find user"); return; }
                    ini.AddSetting(user.SteamID, args[1], "flag");
                    ini.Save();                  
                    pl.Notice("⚑", "You have given " + user.Name + " flag (" + args[1] + ")", 20f);
                    user.Notice("⚑", "You have received  flag (" + args[1] + ")", 20f);        
                }         
            }
            else if (cmd == "flag_invoke")
            {
                if(pl.Admin || Hasflag(pl, "canflag"))
                {
                    if (args.Length != 2)
                    {
                        pl.Notice("⚑", "Syntax /flag_invoke PlayerName FlagName");
                        return;
                    }
                    var user = Fougerite.Server.GetServer().FindPlayer(args[0]);
                    if (user == null) { pl.Notice("⚑", "Failed to find user"); return; }
                    if (ini.ContainsSetting(user.SteamID, args[1]))
                    {
                        ini.DeleteSetting(user.SteamID, args[1]);
                        ini.Save();
                        pl.Notice("⚑", user.Name + " has no longer the flag " + args[1]);
                    }
                    else
                    {
                        pl.Notice("⚑", user.Name + " doesn't have this flag");
                    }
                }
            }
            else if(cmd == "flags_reload")
            {
                if(pl.Admin || Hasflag(pl, "canflag"))
                {
                    ini = new IniParser(Path.Combine(ModuleFolder, "PlayerFlagdb.ini"));
                    pl.Notice("Flags database reloaded");
                }
            }
        }
        bool Hasflag(Flaguser pl, string flag)
        {
            var id = pl.SteamID;
            if (ini.ContainsSetting(id, flag))
            {
                return true;
            }
            return false;

        }

    }
}
