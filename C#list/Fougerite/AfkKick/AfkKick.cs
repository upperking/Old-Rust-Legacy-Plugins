using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//references
using Fougerite;
using Fougerite.Events;
using UnityEngine;
using System.IO;
using AfkPlayer = Fougerite.Player;
using Plugin = Fougerite.Module;

namespace AfkKick
{
    public class AfkKick : Plugin
    {
        public IniParser ini;
        public int AfkTime;
        public float Kickdist = 10f;
        public string KickMessage = "{0} has been kicked for being for to long";


        public override string Name { get { return "AfkKick"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Kicks players if they are afk for to long"; } }
        public override Version Version { get { return new Version("1.0"); } }

        private Dictionary<ulong, Vector3> lastloc = new Dictionary<ulong, Vector3> { };

        public override void Initialize()
        {
            try
            {
                ini = new IniParser(Path.Combine(ModuleFolder, "AfkKick.ini"));
                AfkTime = int.Parse(ini.GetSetting("Options", "AfkTime"));
                KickMessage = ini.GetSetting("Options", "KickMessage");
                Kickdist = float.Parse(ini.GetSetting("Options", "Kickdist"));
            }
            catch (Exception)
            {
                Logger.LogWarning("[AfkKick] AfkKick.ini is corrupted or not found");
            }
            Fougerite.Hooks.OnPlayerSpawned += Spawn;
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnPlayerSpawned -= Spawn;
        }
        void Spawn(AfkPlayer pl, SpawnEvent se)
        {
            if (lastloc.ContainsKey(pl.UID))
            {
                var dict = new Dictionary<string, object>();
                dict["player"] = pl;
                AfkTimer(AfkTime * 60000, dict).Kill();
                lastloc.Remove(pl.UID);
                AfkTimer(AfkTime * 60000, dict).Start();
                lastloc.Add(pl.UID, pl.Location);
            }
            else
            {
                var dict = new Dictionary<string, object>();
                dict["player"] = pl;
                AfkTimer(AfkTime * 60000, dict).Start();
                lastloc.Add(pl.UID, pl.Location);
            }
        }
        public TimedEvent AfkTimer(int timeoutDelay, Dictionary<string, object> args)
        {
            TimedEvent timedEvent = new TimedEvent(timeoutDelay);
            timedEvent.Args = args;
            timedEvent.OnFire += CallBack;
            return timedEvent;
        }
        public void CallBack(TimedEvent e)
        {
            var dictt = e.Args;
            e.Kill();
            AfkPlayer pl = (AfkPlayer)dictt["player"];
            Vector3 loc = lastloc[pl.UID];
            float d = Vector3.Distance(loc, pl.Location);
            if (d < Kickdist)
            {
                lastloc.Remove(pl.UID);
                string msg = KickMessage.Replace("{0}", pl.Name);
                Broadcast(msg);
                pl.Disconnect();
                return;
            }
            else
            {
                lastloc.Remove(pl.UID);
                var dict = new Dictionary<string, object>();
                dict["player"] = pl;
                AfkTimer(AfkTime * 60000, dict).Start();
                lastloc.Add(pl.UID, pl.Location);
            }
        }
        void Broadcast(string message)
        {
            Server.GetServer().BroadcastFrom("AfkKick", message);
        }
    }
}
