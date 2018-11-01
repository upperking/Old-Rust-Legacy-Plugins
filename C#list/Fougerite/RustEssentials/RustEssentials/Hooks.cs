using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace RustEssentials
{
    public class Hooks
    {
       

        public static RustServerManagement management = RustServerManagement.Get();
        public static string SplitQuotesStrings(string args)
        {
            string[] arg;
            arg = Facepunch.Utility.String.SplitQuotesStrings(args);
            return arg.ToString();
        }
        public static PlayerInventory GetInventory(NetUser netuser)
        {
            if(netuser != null)
            {
                return netuser.playerClient.rootControllable.GetComponent<PlayerInventory>();
            }
            return null;
        }
        public static void TeleportPlayerToPlayer(NetUser netuser, NetUser target)
        {
            management.TeleportPlayerToPlayer(netuser.playerClient.netPlayer, target.playerClient.netPlayer);
        }
        public static void TeleportPlayerToLocation(NetUser netuser, Vector3 location)
        {
            management.TeleportPlayerToWorld(netuser.playerClient.netPlayer, location);
        }
        public static void SendMessage(ulong id, string message)
        {
            Fougerite.Server.Cache[id].Message(message);
        }
        public static void Kill(NetUser netuser)
        {
            TakeDamage.KillSelf(netuser.playerClient.controllable.character);
        }
        public static void Hurt(NetUser netuser, float amount)
        {
            TakeDamage.HurtSelf(netuser.playerClient.controllable.character, amount);
        }
    }
}
