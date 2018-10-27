using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using RustBuster2016Server;
using UnityEngine;
using System.Timers;

namespace WarpsServer
{
    public class WarpsModule : Module
    {
        public override string Name => "WarpsServer";
        public override string Author => "ice cold";
        public override string Description => "The server side version of the warp system";
        public override Version Version => new Version("1.0");
        public List<ulong> teleporting;
        public int seconds = 120;
        public Dictionary<ulong, double> Cooldown;
        public override void Initialize()
        {
            teleporting = new List<ulong>();
            Cooldown = new Dictionary<ulong, double>();
            Hooks.OnCommand += Command;
            Hooks.OnPlayerDisconnected += Disconnect;
            RustBuster2016Server.API.OnRustBusterUserMessage += OnRustBusterUserMessage;
        }
        public override void DeInitialize()
        {
            Hooks.OnPlayerDisconnected -= Disconnect;
            Hooks.OnCommand -= Command;
            RustBuster2016Server.API.OnRustBusterUserMessage += OnRustBusterUserMessage;
        }

        private void Disconnect(Player player)
        {
            if(Cooldown.ContainsKey(player.UID))
            {
                Cooldown.Remove(player.UID);
                teleporting.Remove(player.UID);
            }
        }

        public void Command(Player player, string cmd, string[] args)
        {
            if (cmd == "t")
            {
                player.SendConsoleMessage("turnonwarpclientgui");
            }
        }

        public void OnRustBusterUserMessage(API.RustBusterUserAPI user, Message msg)
        {
            if (msg.PluginSender == "WarpsClient")
            {
                if(teleporting.Contains(user.Player.UID)) { user.Player.Message("[color red]You are already teleporting"); return; }
                if (Cooldown.ContainsKey(user.Player.UID))
                {
                    double calc = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds - Cooldown[user.Player.UID];
                    if (calc < seconds)
                    {
                        user.Player.Message("Cooldown Time " + (Math.Round(calc)) + " | " + seconds + " Seconds");
                        return;
                    }              
                }
                string[] split = msg.MessageByClient.Split(',');
                float one = float.Parse(split[0]);
                float two = float.Parse(split[1]);
                float three = float.Parse(split[2]);
                Vector3 loc = new Vector3(one, two, three);
                var data = new Dictionary<string, object>();
                data["user"] = user.Player;
                data["location"] = loc;
                TeleportPlayer(10 * 1000, data).Start();
                user.Player.Notice("Teleporting you in 10 seconds");
                teleporting.Add(user.Player.UID);              

            }
        }
        public TimedEvent TeleportPlayer(int timeoutDelay, Dictionary<string, object> args)
        {
            TimedEvent timedEvent = new TimedEvent(timeoutDelay);
            timedEvent.Args = args;
            timedEvent.OnFire += CallBack;
            return timedEvent;
        }
        public void CallBack(TimedEvent e)
        {
            e.Kill();
            var dict = e.Args;
            Player player = (Player)dict["user"];
            Vector3 loc = (Vector3)dict["location"];
            if (player.IsAlive && player.IsOnline)
            {
                player.TeleportTo(loc);
                Cooldown[player.UID] = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
                teleporting.Remove(player.UID);
            }
        }
    }
}
