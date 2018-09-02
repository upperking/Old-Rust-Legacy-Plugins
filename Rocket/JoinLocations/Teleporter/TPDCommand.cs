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
    public class TPDCommand : IRocketCommand
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
                return "tpd";
            }
        }

        public string Help
        {
            get
            {
                return "Deny incomming teleport requests";
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
                    "Teleporter.TPD"
                };

            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (!Teleporter.Incomming.ContainsKey(player))
            {
                UnturnedChat.Say(player, "You dont have any incomming requests", Color.cyan);
                return;
            }
            UnturnedPlayer target = Teleporter.Incomming[player];
            UnturnedChat.Say(player, "You have denied all teleport request", Color.red);
            UnturnedChat.Say(target, string.Format("{0} has denied your teleport request", player.DisplayName), Color.yellow);

            Teleporter.Requested.Remove(target);
            Teleporter.Incomming.Remove(player);
        }
    }
}
