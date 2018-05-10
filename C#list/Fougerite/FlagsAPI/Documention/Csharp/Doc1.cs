using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Fougerite;

namespace Doc1
{
    public class Doc1 : Fougerite.Module
    {
        public IniParser flags;


        public override string Name { get { return "TestFlags"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "testing"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public override void Initialize()
        {
            flags = new IniParser(Directory.GetCurrentDirectory() + "\\save\\FlagsAPI\\PlayerFlagdb.ini");

            Fougerite.Hooks.OnCommand += Test;
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnCommand -= Test;
        }
        void Test(Fougerite.Player pl, string cmd, string[] args)
        {
            if (cmd == "test")
            {
                if (Hasflag(pl, "testflag")) 
                {
                    //Further magic                   
                }
                else
                {
                    pl.Message("The flag > testflag < is required for this command");
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
