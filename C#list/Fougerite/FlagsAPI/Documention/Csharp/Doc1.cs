using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Fougerite;
using FlagsAPI;

namespace TestFlags
{
    public class Class1 : Fougerite.Module
    {
        public override string Name { get { return "TestFlags"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "testing"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public override void Initialize()
        {
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
                if (FlagsAPI.API.Hasflag(pl, "testflag"))
                {
                   FlagsAPI.API.RegisterFlag("test2");              
                }
                else
                {
                    pl.Message("The flag > testflag < is required for this command");
                }
            }
            else if(cmd == "test2")
            {
                if(FlagsAPI.API.Hasflag(pl, "testflag"))
                {
                    FlagsAPI.API.RemoveFlag("test2");
                }
            }
            else if(cmd == "getflag")
            {
                Player target = Server.GetServer().FindPlayer(args[0]);
                pl.MessageFrom(Name, target.Name + " has " + FlagsAPI.API.GetFlag(target, args[1]));
            }
        }
    }
}
