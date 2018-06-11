using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Flag = Fougerite.Module;
using System.IO;

namespace FlagsAPI
{
    public class Core : Flag
    {
        public static IniParser ini;
        public static IniParser list;

        public override string Name { get { return "FlagsAPI"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Manage flags on players"; } }
        public override Version Version { get { return new Version("1.1"); } }


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
            if(!File.Exists(Path.Combine(ModuleFolder, "Flags.list")))
            {
                File.Create(Path.Combine(ModuleFolder, "Flags.list")).Dispose();
                list = new IniParser(Path.Combine(ModuleFolder, "Flags.list"));
                list.Save();
            }
            else
            {
                list = new IniParser(Path.Combine(ModuleFolder, "Flags.list"));
            }
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnCommand -= Command;
        }
        void Command(Player pl, string cmd, string[] args)
        {
            if (cmd == "flag")
            {
                if (pl.Admin || API.Hasflag(pl, "canflag"))
                {
                    pl.Notice("⚑", "FlagsAPI brought by ice cold", 20f);
                    pl.MessageFrom(Name, "FlagsAPI 1.1 by ice cold");
                    pl.MessageFrom(Name, "/flag_add Player flagname - gives the player the flagname");
                    pl.MessageFrom(Name, "/flag_invoke Player flagname - removes the flag in player");
                    pl.MessageFrom(Name, "/flag_register Flagname - register a new flag");
                }
                else
                {
                    pl.MessageFrom(Name, "You dont have the > canflag < flag");
                }
            }
            else if (cmd == "flag_add")
            {
                if(pl.Admin || API.Hasflag(pl, "canflag"))
                {
                    if (args.Length != 2)
                    {
                        pl.Notice("⚑", "Syntax /flag_add PlayerName FlagName");
                        return;
                    }
                    if(list.ContainsSetting("Flags", args[1]))
                    {
                        var user = Fougerite.Server.GetServer().FindPlayer(args[0]);
                        if (user == null) { pl.Notice("⚑", "Failed to find user"); return; }
                        ini.AddSetting(user.SteamID, args[1], args[1]);
                        ini.Save();
                        pl.Notice("⚑", "You have given " + user.Name + " flag (" + args[1] + ")", 20f);
                        user.Notice("⚑", "You have received  flag (" + args[1] + ")", 20f);
                    }
                    else
                    {
                        pl.MessageFrom(Name, "You must register this flag first by doing /flag_register " + args[1]);
                    }
                }         
            }
            else if (cmd == "flag_invoke")
            {
                if(pl.Admin || API.Hasflag(pl, "canflag"))
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
                if(pl.Admin || API.Hasflag(pl, "canflag"))
                {
                    ini = new IniParser(Path.Combine(ModuleFolder, "PlayerFlagdb.ini"));
                    list = new IniParser(Path.Combine(ModuleFolder, "Flags.list"));
                    pl.Notice("Flags database reloaded");
                }
            }
            else if(cmd == "flag_register")
            {
                if(pl.Admin || API.Hasflag(pl, "canflag"))
                {
                    API.RegisterFlag(args[0]);
                    pl.MessageFrom(Name, "The flag (" + args[0] + ") was succestfully registered");
                }
            }
        }     
    }
}
