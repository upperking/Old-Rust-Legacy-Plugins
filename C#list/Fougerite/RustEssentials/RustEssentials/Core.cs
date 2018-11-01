using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.Events;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Timers;
using User = Fougerite.Player;

namespace RustEssentials
{
    public class Core : Module
    {
        public List<User> playerlist = new List<User>();
        public IniParser ini;
        public StreamWriter warpswriter;
        public List<string> commands;
        public override string Name { get { return "RustEssentials"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "A multi core plugin"; } }
        public override Version Version { get { return new Version("1.0B"); } }
        public override void Initialize()
        {
            commands = new List<string>();
            Fougerite.Hooks.OnPlayerConnected += Connected;
            Fougerite.Hooks.OnCommand += Command;
            Fougerite.Hooks.OnPluginInit += PluginInit;
            Fougerite.Hooks.OnSteamDeny += Deny;
            Fougerite.Hooks.OnResourceSpawned += ResourceSpawn;
        }

        private void ResourceSpawn(ResourceTarget resource)
        {
            // Calls the resource hook at Gather.cs
            RustEssentials.Plugins.Gather.ResourceNode(resource);
        }

        private void Connected(User player)
        {
            CheckForPlayerData(player);
            if(!playerlist.Contains(player))
            {
                playerlist.Add(player);
            }
            else
            {
                playerlist.Remove(player);
                Helper.LogError("[RustEssentials]KICKING " + player.Name + " becaus he already existed in the player list list");
                player.Disconnect();          
            }
        }

        private void Deny(SteamDenyEvent sde)
        {
            if(sde.NetUser != null && sde.ClientConnection != null)
            {
                if(sde.Reason == "Facepunch_Connector_Cancelled")
                {
                    sde.ForceAllow = true;
                }
                else if(sde.Reason == "Denying entry to " + sde.NetUser.userID + " because they're already connected")
                {
                    sde.NetUser.Kick(NetError.Facepunch_Kick_Violation, true);
                }
            }
        }

        private void PluginInit()
        {
            // Calls the initialize hook on all plugins
            RustEssentials.Plugins.TPR.Start();
        }

        public void Command(User player, string cmd, string[] args)
        {
            NetUser netuser = player.PlayerClient.netUser;

            switch (cmd)
            {
                case "tpr":
                    {
                        RustEssentials.Plugins.TPR.ExecuteTPR(netuser, args);
                        break;
                    }
                case "tpa":
                    {
                        RustEssentials.Plugins.TPR.ExecuteTPA(netuser, args);
                        break;
                    }
                case "warps":
                    {

                    }
            }

        }

        public override void DeInitialize()
        {
            throw new NotImplementedException();
        }
      
    }
}
