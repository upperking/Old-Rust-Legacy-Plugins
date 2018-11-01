using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RustEssentials.Plugins
{
    public class TPR
    {
        static int terrain;
        static Vector3 Down = new Vector3(0f, -0.4f, 0f);
        static RaycastHit Ray;
        public  static Dictionary<ulong, double> Cooldown;
        public static Dictionary<NetUser, NetUser> Requested;
        public static Dictionary<NetUser, NetUser> Incomming;

        public static void Start()
        {
            terrain = LayerMask.GetMask(new string[] { "Static" });
            Cooldown = new Dictionary<ulong, double>();
            Requested = new Dictionary<NetUser, NetUser>();
            Incomming = new Dictionary<NetUser, NetUser>();

        }
        public static void ExecuteTPR(NetUser netuser, string[] args)
        {
            if(args.Length != 2)
            {

            }
            string arg = Hooks.SplitQuotesStrings(args[0]);
            string arg1 = Hooks.SplitQuotesStrings(args[1]);

            if(Cooldown.ContainsKey(netuser.userID))
            {
                double calc = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds - Cooldown[netuser.userID];
                if (calc < TprCooldown)
                {
                    Hooks.SendMessage(netuser.userID, "Cooldown Time " + (Math.Round(calc)) + " | " + TprCooldown + " Seconds");
                    return;
                }
            }
            Cooldown.Remove(netuser.userID);


        }
        public static void ExecuteTPA(NetUser netuser, string[] args)
        {

        }
    }
}
