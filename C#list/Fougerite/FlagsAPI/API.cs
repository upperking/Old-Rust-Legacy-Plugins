using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;

namespace FlagsAPI
{
    public class flag
    {
        public static bool HasFlag(Player pl, string flag)
        {
            if (pl.IsOnline)
            {
                if (Core.ini.ContainsSetting(pl.SteamID, flag))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public static void RemoveFlag(Player pl, string flag)
        {
            if (pl.IsOnline)
            {
                if (flag != string.Empty)
                {
                    Core.ini.DeleteSetting(pl.SteamID, flag);
                    Core.ini.Save();
                }
            }
        }
        public static void RegisterFlag(string flag)
        {
            if (flag != string.Empty)
            {
                Core.list.AddSetting("Flags", flag, "flag");
                Core.list.Save();
            }

        }
        public static void RemoveFlag(string flag)
        {
            if (flag != string.Empty)
            {
                Core.list.DeleteSetting("Flags", flag);
                Core.list.Save();
            }
        }
        public static string GetFlag(Player pl, string flag)
        {
            if (flag != string.Empty)
            {
                var l = Core.ini.GetSetting(pl.SteamID, flag);
                if (Core.ini.ContainsSetting(pl.SteamID, flag))
                {
                    return l;
                }
                return null;
            }
            return null;
        }
    }
}
