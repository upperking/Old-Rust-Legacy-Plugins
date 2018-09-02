using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;

namespace Teleporter
{
    public class Teleporter : RocketPlugin<Configuration>
    {
        public static Dictionary<UnturnedPlayer, UnturnedPlayer> Requested;
        public static Dictionary<UnturnedPlayer, UnturnedPlayer> Incomming;
        public static Dictionary<string, double> Cooldown;
        public static Teleporter instance;

        protected override void Load()
        {
            Requested = new Dictionary<UnturnedPlayer, UnturnedPlayer>();
            Incomming = new Dictionary<UnturnedPlayer, UnturnedPlayer>();
            Cooldown = new Dictionary<string, double>();
            instance = this;

            U.Events.OnPlayerDisconnected += Disconnect;

        }
        protected override void Unload()
        {
            U.Events.OnPlayerDisconnected -= Disconnect;
            instance = null;
            Requested = null;
            Incomming = null;
            Cooldown = null;
        }

        public void Disconnect(UnturnedPlayer player)
        {
            if(Requested.ContainsKey(player))
            {
                Requested.Remove(player);
            }
            if(Incomming.ContainsKey(player))
            {
                Incomming.Remove(player);
            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"TprSend", "You have send a teleport request to {target}" },
                    {"TprIncomming", "You have a incomming teleport request from {sender}" },
                    {"TprAccept ", "You have accepted the teleport request of {sender}" },
                    {"TprAccepted", "{target} has accepted your teleport request" }
                };
            }
        }            
    }
}
