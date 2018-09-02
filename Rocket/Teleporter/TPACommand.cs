using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using UnityEngine;
using System.Timers;

namespace Teleporter
{
    public class TPACommand : IRocketCommand
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
                return "tpa";
            }
        }

        public string Help
        {
            get
            {
                return "Accept teleport requests";
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
                    "Teleporter.TPA"
                };

            }
        }

        public void Execute(IRocketPlayer caller, string[] args)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if(!Teleporter.Incomming.ContainsKey(player)) { UnturnedChat.Say(player, "You dont have any incomming requests", Color.red); return; }
            UnturnedPlayer sender = Teleporter.Incomming[player];
            UnturnedChat.Say(player, Teleporter.instance.Translate("TprAccept").Replace("{sender}", sender.DisplayName));
            UnturnedChat.Say(sender, Teleporter.instance.Translate("TprAccepted").Replace("{target}", player.DisplayName));

            Timer timer = new Timer();
            timer.Elapsed += EndDelay(player, sender);
            timer.Interval = Teleporter.instance.Configuration.Instance.TprDelay * 1000;
            timer.Enabled = true;
            timer.AutoReset = false;

        }

        private ElapsedEventHandler EndDelay(UnturnedPlayer player, UnturnedPlayer sender)
        {

            sender.Teleport(player);
            Teleporter.Requested.Remove(sender);
            Teleporter.Incomming.Remove(player);

            //Cooldown method
            Teleporter.Cooldown[sender.Id] = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;

            return null;
        }
    }
}
