using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Fougerite;

namespace TimeManager
{
    public class TimeManager : Module
    {
        public IniParser ini;

        public override string Name { get { return "TimeManager"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Allows you to freeze/change the time on your server"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public int FreezeTime;
        public int DayLenght;
        public int NightLenght;

        public override void Initialize()
        {
            Hooks.OnCommand += Command;
            Hooks.OnServerLoaded += Loaded;
            CheckConfig();
        }
        public override void DeInitialize()
        {
            Hooks.OnCommand -= Command;
            Hooks.OnServerLoaded -= Loaded;
        }

        public void Command(Fougerite.Player pl, string cmd, string[] args)
        {
            if (cmd == "time")
            {
                if (pl.Admin)
                {
                    if (args.Length == 0)
                    {
                        pl.MessageFrom(Name, "/time number - changes the time to that number");
                        pl.MessageFrom(Name, "/freezetime number - freeze the time on the number (0 = disable time freeze)");
                        pl.MessageFrom(Name, "/daylenght number - changes daylenght to number");
                        pl.MessageFrom(Name, "/nightlenght number - changes nightlenght to number");
                        return;
                    }
                }
            }
            else if (cmd == "freezetime")
            {
                if (pl.Admin)
                {
                    if (args.Length == 0)
                    {
                        pl.MessageFrom(Name, "Usage /freezetime number");
                        pl.MessageFrom(Name, "Current freeze time = " + FreezeTime);
                        return;
                    }
                    int value = Convert.ToInt32(args[0]);
                    if (int.TryParse(args[0], out value))
                    {
                        World.GetWorld().Time = value;
                        ini.SetSetting("Config", "FreezeTime", args[0]);
                        ini.Save();
                    }
                    else
                    {
                        pl.MessageFrom(Name, "Oops make sure that the value is a number");
                    }
                }
                else
                {
                    pl.MessageFrom(Name, "Dont you dare trying to change the time you filthy peasant");
                }
            }
            else if (cmd == "daylenght")
            {
                if (pl.Admin)
                {
                    if (args.Length == 0)
                    {
                        pl.MessageFrom(Name, "Usage /daylenght number");
                        pl.MessageFrom(Name, "Current daylenght = " + env.daylength);
                        return;
                    }
                    int value = Convert.ToInt32(args[0]);
                    if (int.TryParse(args[0], out value))
                    {
                        pl.MessageFrom(Name, "Daylenght has been changed from " + env.daylength + " to " + value);
                        env.daylength = value;
                        ini.SetSetting("Config", "DayLenght", args[0]);
                        ini.Save();
                    }
                    else
                    {
                        pl.MessageFrom(Name, "Oops make sure that the value is a number");
                    }
                }
            }
            else if (cmd == "nightlenght")
            {
                if (pl.Admin)
                {
                    if (args.Length == 0)
                    {
                        pl.MessageFrom(Name, "Usage /nightlenght number");
                        pl.MessageFrom(Name, "Current nightlenght = " + env.daylength);
                        return;
                    }
                    int value = Convert.ToInt32(args[0]);
                    if (int.TryParse(args[0], out value))
                    {
                        pl.MessageFrom(Name, "Nightlenght has been changed from " + env.nightlength + " to " + value);
                        env.nightlength = value;
                        ini.SetSetting("Config", "NightLenght", args[0]);
                        ini.Save();
                    }
                    else
                    {
                        pl.MessageFrom(Name, "Oops make sure that the value is a number");
                    }
                }
            }
        }

        public void Loaded()
        {
            if (FreezeTime > 0)
            {
                World.GetWorld().Time = FreezeTime;
                Timerchange(3 * 1000, null).Start();
                Logger.Log("The server time has been frozen to " + FreezeTime);
                
            }
            else
            {
                //use Assembly-CSharp.dll
                env.nightlength = NightLenght;
                env.daylength = DayLenght;
            }
        }
        public TimedEvent Timerchange(int timeoutDelay, Dictionary<string, object> args)
        {
            TimedEvent timedEvent = new TimedEvent(timeoutDelay);
            timedEvent.Args = args;
            timedEvent.OnFire += CallBack;
            return timedEvent;
        }
        public void CallBack(TimedEvent e)
        {
            World.GetWorld().Time = FreezeTime;
            Logger.Log("Time changed to " + FreezeTime);
        }
        private void CheckConfig()
        {
            try
            {
                ini = new IniParser(Path.Combine(ModuleFolder, "Config.config"));
                FreezeTime = int.Parse(ini.GetSetting("Config", "FreezeTime"));
                DayLenght = int.Parse(ini.GetSetting("Config", "DayLenght"));
                NightLenght = int.Parse(ini.GetSetting("Config", "NightLenght"));
            }
            catch (Exception)
            {
                Logger.LogWarning("[TimeManager] Failed to parse config your its not found corrupted");
            }
        }
    }
}
