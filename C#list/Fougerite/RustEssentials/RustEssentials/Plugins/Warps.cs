using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RustEssentials.Plugins
{
    public class Warps
    {
        public static Dictionary<string, string> warpslist;
        public static void Start()
        {
            warpslist = new Dictionary<string, string>();
        }
        public static void ExecuteWarps(NetUser netuser)
        {
            Hooks.SendMessage(netuser.userID, "Warps list");
            foreach(string x in warpslist.Keys)
            {
                Hooks.SendMessage(netuser.userID, warpslist[x]);
            }
        }
    }
}
