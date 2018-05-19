using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using UnityEngine;
using System.IO;

namespace AdminLocations
{
    public class AdminLocations : Fougerite.Module
    {
        public IniParser ini;
        public IniParser flags;

        public string FLAG_USE = "adminlocations.use";

        public override string Name { get { return "AdminLocations"; } }
        public override string Author { get { return "ice cold, jakkee"; } }
        public override string Description { get { return "AdminLocations original Python version by jakkee recoded to C# by ice cold"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public override void Initialize()
        {
            Fougerite.Hooks.OnCommand += Command;

            flags = new IniParser(Directory.GetCurrentDirectory() + "\\save\\FlagsAPI\\PlayerFlagdb.ini");

            if (!File.Exists(Path.Combine(ModuleFolder, "Locations.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Locations.ini")).Dispose();
                ini = new IniParser(Path.Combine(ModuleFolder, "Locations.ini"));
                ini.Save();
            }
            else
            {
                ini = new IniParser(Path.Combine(ModuleFolder, "Locations.ini"));
            }
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnCommand -= Command;
        }
        public void Command(Player pl, string cmd, string[] args)
        {
            if (cmd == "tploc")
            {
                if (pl.Admin || Hasflag(pl, FLAG_USE))
                {
                    if (args.Length == 0)
                    {
                        pl.MessageFrom(Name, "Try out /tploc name");
                        string[] en = ini.EnumSection("Locations");
                        {
                            foreach(var locs in en)
                            {
                                
                                pl.MessageFrom(Name, locs);
                            }
                        }                      
                    }
                    else
                    {
                        if (ini.ContainsSetting("Locations", args[0]))
                        {
                            string j = ini.GetSetting("Locations", args[0]);
                            Vector3 loc = Util.GetUtil().ConvertStringToVector3(j); // makes a Vector3 of string
                            pl.TeleportTo(loc);
                            pl.InventoryNotice(args[0]);
                        }
                        else
                        {
                            pl.MessageFrom(Name, "There is no location called " + args[0]);
                        }
                    }
                }
            }
            else if (cmd == "tplocadd")
            {
                if (pl.Admin || Hasflag(pl, FLAG_USE))
                {
                    if (args.Length == 0)
                    {
                        pl.MessageFrom(Name, "Syntax /tplocadd name");
                        return;
                    }
                    if (ini.ContainsSetting("Locations", args[0]))
                    {
                        pl.MessageFrom(Name, "There is already a location called " + args[0]);
                    }
                    else
                    {
                        ini.AddSetting("Locations", args[0], pl.Location.ToString());
                        pl.MessageFrom(Name, "New location called " + args[0] + " added at " + pl.Location);
                        ini.Save();
                    }
                }
            }
            else if (cmd == "tplocremove")
            {
                if (pl.Admin || Hasflag(pl, FLAG_USE))
                {
                    if (args.Length == 0)
                    {
                        pl.MessageFrom(Name, "Syntax /tplocremove name");
                        return;
                    }
                    if (ini.ContainsSetting("Locations", args[0]))
                    {
                        ini.DeleteSetting("Locations", args[0]);
                        ini.Save();
                        pl.MessageFrom(Name, "The location " + args[0] + " has been deleted");
                    }
                }
            }
        }
        bool Hasflag(Fougerite.Player pl, string flag)
        {
            var id = pl.SteamID;
            if (flags.ContainsSetting(id, flag))
            {
                return true;
            }
            return false;
        }
    }
}
