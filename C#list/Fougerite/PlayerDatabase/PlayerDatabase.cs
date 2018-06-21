using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using UnityEngine;
using System.IO;

using User = Fougerite.Player;

namespace PlayerDatabase
{
    public class PlayerDatabase : Module
    {
        public static IniParser list;
        public static string name = "[PlayerDatabase 1.0]";
        public override string Name { get { return "PlayerDatabase"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "This plugin stores all player information into a file. Special Hooks to let other plugins call them"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public PlayerClient player;

        public override void Initialize()
        {
            Hooks.OnPlayerConnected += Connected;
            Hooks.OnPlayerDisconnected += Disconnected;
            Hooks.OnCommand += Command;

            DataConfig();
        }
        public override void DeInitialize()
        {

            Hooks.OnPlayerConnected += Connected;
            Hooks.OnPlayerDisconnected += Disconnected;
            Hooks.OnCommand += Command;
        }
        private void DataConfig()
        {
            if(!File.Exists(Path.Combine(ModuleFolder, "PlayerDatabase.list")))
            {
                File.Create(Path.Combine(ModuleFolder, "PlayerDatabase.list")).Dispose();
                list = new IniParser(Path.Combine(ModuleFolder, "PlayerDatabase.list"));
                list.Save();
            }
            else
            {
                list = new IniParser(Path.Combine(ModuleFolder, "PlayerDatabase.list"));
            }
        }
        public void Connected(User pl)
        {
            var id = pl.SteamID;
            if(pl == null) { Logger.LogWarning(name + " a player was null"); return; }
            if(!list.ContainsSetting(id, "GameID")) { Logger.Log(name + " " +  pl.Name + " - " + id + " has been added to the PlayerDatabase"); }
            list.AddSetting(id, "Name", pl.Name);
            list.AddSetting(id, "GameID", id);
            list.AddSetting(id, "IP", pl.IP);
            list.AddSetting(id, "LastJoined", DateTime.Now.ToString());
            list.AddSetting(id, "Online", "yes");
            if(player.netUser.admin)
            {
                list.AddSetting(id, "IsAdmin", "true");
            }
            else
            {
                list.AddSetting(id, "IsAdmin", "false");
            }
            list.Save();
        }
        public void Disconnected(User pl)
        {
            list.AddSetting(pl.SteamID, "LastLeft", DateTime.Now.ToString());
            list.AddSetting(pl.SteamID, "Online", "no");
            list.Save();
        }
        public void Command(User pl, string cmd, string[] args)
        {
            if (cmd == "findinfo")
            {
                if (pl.Admin || pl.Moderator)
                {
                    if(args.Length != 1)
                    {
                        pl.MessageFrom(name, "Syntax /findinfo PlayerName");
                        return;
                    }
                    User target = Server.GetServer().FindPlayer(args[0]);
                    if(target == null) { pl.MessageFrom(Name, "Couldn't find the target user"); return; }
                    pl.MessageFrom(Name, "Name " + list.GetSetting(target.SteamID, "Name"));
                    pl.MessageFrom(Name, "GameID " + list.GetSetting(target.SteamID, "GameID"));
                    pl.MessageFrom(Name, "IP " + list.GetSetting(target.SteamID, "IP"));
                    pl.MessageFrom(Name, "LastJoined " + list.GetSetting(target.SteamID, "LastJoined"));
                    pl.MessageFrom(Name, "IsAdmin " + list.GetSetting(target.SteamID, "isAdmin"));
                }
            }
        }
        public static string GetName(User pl)
        {
            if(pl != null)
            {
                return list.GetSetting(pl.SteamID, "Name");
            }
            return null;
        }
        public static bool IsAdmin(User pl)
        {
            if(pl != null)
            {
                if(list.GetSetting(pl.SteamID, "IsAdmin") == "true")
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public static void SetData(User pl, string selection, string data)
        {
            if(pl != null)
            {
                list.AddSetting(pl.SteamID, selection, data);
                list.Save();
            }
        }
    }
}
