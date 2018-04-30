using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.Events;
using System.IO;

namespace SpawnShield
{
    public class SpawnClass : Fougerite.Module
    {
        private IniParser Settings;
        public int ShieldTime = 10;
        public bool RustBusterSupport;
        public string ActiveMessage = "Your spawn shield is enabled";
        public string DeactiveMessage = "Your spawn shield was ended";

        public override string Name { get { return "SpawnShield"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Gives the player the x amount seconds godmode when he spawns"; } }
        public override Version Version { get { return new Version("1.0"); } }

        private List<ulong> onshield = new List<ulong> { };

        public override void Initialize()
        {
            Fougerite.Hooks.OnPlayerSpawned += Spawn;
            Fougerite.Hooks.OnPlayerHurt += Hurt;
            Fougerite.Hooks.OnPlayerDisconnected += Disconnected;

            if (!File.Exists(Path.Combine(ModuleFolder, "Config.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Config.ini")).Dispose();
                Settings = new IniParser(Path.Combine(ModuleFolder, "Config.ini"));
                Settings.AddSetting("Options", "ShieldTime", ShieldTime.ToString());
                Settings.AddSetting("Options", "RustBusterSupport", "false");
                Settings.AddSetting("Messages", "ActiveMessage", "Your spawn shield is enabled");
                Settings.AddSetting("Messages", "DeactiveMessage", "Your spawn shield was ended");
                Settings.Save();
            }
            else
            {
                Settings = new IniParser(Path.Combine(ModuleFolder, "Config.ini"));
                ShieldTime = int.Parse(Settings.GetSetting("Options", "ShieldTime"));
                RustBusterSupport = bool.Parse(Settings.GetSetting("Options", "RustBusterSupport"));
                ActiveMessage = Settings.GetSetting("Messages", "ActiveMessage");
                DeactiveMessage = Settings.GetSetting("Messages", "DeactiveMessage");
            }
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnPlayerSpawned -= Spawn;
            Fougerite.Hooks.OnPlayerHurt -= Hurt;
            Fougerite.Hooks.OnPlayerDisconnected -= Disconnected;
        }
        public void Spawn(Fougerite.Player pl, SpawnEvent se)
        {
            var dict = new Dictionary<string, object>();
            dict["inshield"] = pl;
            onshield.Add(pl.UID);
            pl.MessageFrom("SpawnShield", ActiveMessage);
            ShieldTimer(ShieldTime * 1000, dict).Start();
            if (RustBusterSupport)
            {
                pl.SendConsoleMessage("shieldgotalk");
            }
        }
        public void Hurt(HurtEvent he)
        {
            if (he.AttackerIsPlayer && he.VictimIsPlayer)
            {
                Fougerite.Player attacker = (Fougerite.Player)he.Attacker;
                Fougerite.Player victim = (Fougerite.Player)he.Victim;
                if (onshield.Contains(attacker.UID))
                {
                    he.DamageAmount = 0;
                    return;
                }
                else if (onshield.Contains(victim.UID))
                {
                    he.DamageAmount = 0;
                    return;
                }
            }
        }
        public void Disconnected(Fougerite.Player pl)
        {
            if (onshield.Contains(pl.UID))
            {
                onshield.Remove(pl.UID);
            }
        }
#region timers
        public TimedEvent ShieldTimer(int timeoutDelay, Dictionary<string, object> args)
        {
            TimedEvent timedEvent = new TimedEvent(timeoutDelay);
            timedEvent.Args = args;
            timedEvent.OnFire += CallBack;
            return timedEvent;
        }
        public void CallBack(TimedEvent e)
        {
            var dict = e.Args;
            e.Kill();
            Fougerite.Player pl = (Fougerite.Player)dict["inshield"];
            onshield.Remove(pl.UID);
            pl.MessageFrom("SpawnShield", DeactiveMessage);
            if (RustBusterSupport)
            {
                pl.SendConsoleMessage("shieldgotalk2");
            }
        }
    }
}
#endregion
