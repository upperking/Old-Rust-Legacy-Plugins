using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using UnityEngine;

namespace Teleporter
{
    public class TPRCommand : IRocketCommand
    {
        public int cooldown = Teleporter.instance.Configuration.Instance.TprCooldown;

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
                return "tpr";
            }
        }

        public string Help
        {
            get
            {
                return "Send teleport request to players";
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
                    "Teleporter.TPR"
                };

            }
        }

        public void Execute(IRocketPlayer caller, string[] args)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if(args.Length != 1)
            {
                UnturnedChat.Say(player, "/tpr PlayerName - Send a teleport request");
                UnturnedChat.Say(player, "/tpa - accept a teleport request");
                UnturnedChat.Say(player, "/tpd - deny all incomming teleport requests");
                UnturnedChat.Say(player, "/tpc - cancel all teleport requests");
                return;
            }
            if(Teleporter.Cooldown.ContainsKey(player.Id))
            {
                double calc = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds - Teleporter.Cooldown[player.Id];
                if(calc < cooldown)
                {
                    UnturnedChat.Say(player, "Cooldown time " + (Math.Round(calc)) + " | " + cooldown);
                    return;
                }
            }
            Teleporter.Cooldown.Remove(player.Id);
            UnturnedPlayer target = UnturnedPlayer.FromName(args[0]);
            if(target == null) { UnturnedChat.Say(player, "Couldn't find the target user", Color.red); return; }
            if(Teleporter.Requested.ContainsKey(player)) { UnturnedChat.Say(player, "You are already pending a teleport request", Color.red); return; }
            if(Teleporter.Incomming.ContainsKey(target)) { UnturnedChat.Say(player, target.DisplayName + " is already having a teleport request", Color.red); return; }
            if(target.IsInVehicle) { UnturnedChat.Say(player, target.DisplayName + " is in a vehicle", Color.red); return; }
            if(player.IsInVehicle) { UnturnedChat.Say(player, "You cannot send request while driving", Color.red); return; }

            UnturnedChat.Say(player, Teleporter.instance.Translate("TprSend").Replace("{target}", target.DisplayName));
            UnturnedChat.Say(target, Teleporter.instance.Translate("TprIncomming").Replace("{sender}", player.DisplayName));

            Teleporter.Requested.Add(player, target);
            Teleporter.Incomming.Add(target, player);         
        }
    }
}
