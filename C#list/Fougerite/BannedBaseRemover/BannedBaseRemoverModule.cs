using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using UnityEngine;

namespace BannedBaseRemover
{
    public class BannedBaseRemoverModule : Module
    {
        public override string Name { get { return "BannedBaseRemover"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Remove all banned user objects with 1 single command"; } }
        public override Version Version { get { return new Version("1.0"); } }

        World world = World.GetWorld();
        DataStore ds = DataStore.GetInstance();

        public override void Initialize()
        {
            Hooks.OnCommand += Command;
        }
        public override void DeInitialize()
        {
            Hooks.OnCommand -= Command;
        }
        void Command(Player pl, string cmd, string[] args)
        {
            if(cmd == "rbb")
            {
                if(pl.Admin)
                {
                    pl.MessageFrom(Name, "Locating banned user bases/Deployables...");
                    foreach (var x in world.Entities)
                    {
                        if(x.OwnerID != null)
                        {
                            if(ds.ContainsKey("Ids", x.OwnerID))
                            {
                                x.Destroy();
                            }
                        }
                    }
                    pl.MessageFrom(Name, "Removed all banned user objects");
                }
            }
        }
    }
}
