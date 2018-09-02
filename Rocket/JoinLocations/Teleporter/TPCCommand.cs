using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using UnityEngine;


namespace Teleporter
{
   public class TPCCommand :IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public string Name
        {
            get
            {
                return "tpc";
            }
        }

        public string Help
        {
            get
            {
                return "Cancel your teleport request";
            }
        }

        public string Syntax
        {
            get
            {
                return "Send, Cancel, deny, accept";
            }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>();
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>()
                {
                    "Teleporter.TPC"
                };

            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if(!Teleporter.Requested.ContainsKey(player))
            {
                UnturnedChat.Say(player, "You are not pending a request", Color.cyan);
                return;
            }
            UnturnedPlayer target = Teleporter.Requested[player];

            UnturnedChat.Say(player, "You have canceled all pending requests", Color.yellow);
            UnturnedChat.Say(target, string.Format("{0} has canceled his teleport request", player.DisplayName), Color.yellow);
            Teleporter.Requested.Remove(player);
            Teleporter.Incomming.Remove(target);

        }
    }
}
