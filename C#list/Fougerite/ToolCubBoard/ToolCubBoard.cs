using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using System.IO;
using RustPP.Social;

namespace ToolCubBoard
{
    public class ToolCubBoard : Fougerite.Module
    {
        public bool HasRustPP = false;
        private IniParser Settings;
        public bool AllowBarricade;
        public bool AllowRamps;
        public bool AllowBoxes;
        public bool AllowSleepingBags;
        public bool AllowBeds;
        public static float RadiusCheck = 25f;
        private string orange = "[color orange]";
        private string aqua = "[color aqua]";
        public override string Name { get { return "ToolCubBoard"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Inspired on the TC system from rust experimental"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public override void Initialize()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "Settings.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Settings.ini")).Dispose();
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                Settings.AddSetting("Settings", "AllowBarricade", "true");
                Settings.AddSetting("Settings", "AllowBoxes", "true");
                Settings.AddSetting("Settings", "AllowSleepingBags", "true");
                Settings.AddSetting("Settings", "AllowBeds", "true");
                Settings.AddSetting("Settings", "AllowRamps", "true");
                Settings.AddSetting("Radius", "Radius", RadiusCheck.ToString());
                Settings.Save();
            }
            else
            {
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                AllowBarricade = bool.Parse(Settings.GetSetting("Settings", "AllowBarricade"));
                AllowBoxes = bool.Parse(Settings.GetSetting("Settings", "AllowBoxes"));
                AllowSleepingBags = bool.Parse(Settings.GetSetting("Settings", "AllowSleepingBags"));
                AllowBeds = bool.Parse(Settings.GetSetting("Settings", "AllowBeds"));
                AllowRamps = bool.Parse(Settings.GetSetting("Settings", "AllowRamps"));
            }
            Fougerite.Hooks.OnEntityDeployedWithPlacer += OnEntityDeployed;
            Fougerite.Hooks.OnServerInit += OnServerInit;
            Fougerite.Hooks.OnModulesLoaded += OnModulesLoaded;
        }
        public void OnModulesLoaded()
        {
            if (Fougerite.Server.GetServer().HasRustPP)
            {
                Logger.Log("Rust++ was founded now this plugin can be used");
                HasRustPP = true;
            }
            else
            {
                Logger.LogWarning("RustPP is not on this server now you cannot use this plugin");
                HasRustPP = false;
            }
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnEntityDeployedWithPlacer -= OnEntityDeployed;
            Fougerite.Hooks.OnServerInit -= OnServerInit;
            Fougerite.Hooks.OnModulesLoaded -= OnModulesLoaded;
        }
        public void OnServerInit()
        {
            Logger.Log("*******************************************************************************************************");
            Logger.Log("Tool Cub Board System 1.0 by ice cold has been loaded");
            Logger.Log("If someone is not in the friendlist of owner he cannot place structures");
            Logger.Log("*******************************************************************************************************");
        }
        public void OnEntityDeployed(Fougerite.Player pl, Fougerite.Entity e, Fougerite.Player actualplacer)
        {
            HasRustPP = true;
            if (HasRustPP == true)
            {
                foreach (Fougerite.Entity xx in Util.GetUtil().FindEntitiesAround(e.Location, 25f))
                {
                    if (e.Name == "WoodFoundation")
                    {
                        if (xx.Name == "WoodFoundation")
                        {
                            if (xx.OwnerID == actualplacer.SteamID)
                            {
                            }
                            else
                            {
                                var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                if (friendc.ContainsKey(xx.UOwnerID))
                                {
                                    var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                    bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                    if (isfriend)
                                    {
                                    }
                                    else
                                    {
                                        e.Destroy();
                                        {
                                            actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                            actualplacer.Inventory.AddItem(e.Name, 1);
                                            actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                        }
                                    }
                                }
                            }
                        }
                        else if (e.Name.ToLower().Contains("bench"))
                        {
                            if (xx.OwnerID == actualplacer.SteamID)
                            {
                                break;
                            }
                            else
                            {
                                var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                if (friendc.ContainsKey(xx.UOwnerID))
                                {
                                    var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                    bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                    if (isfriend)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        e.Destroy();
                                        {
                                            actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                            actualplacer.Inventory.AddItem(e.Name, 1);
                                            actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                        }
                                    }
                                }
                            }
                        }
                        else if (e.Name.ToLower().Contains("spike"))
                        {
                            if (xx.OwnerID == actualplacer.SteamID)
                            {
                                break;
                            }
                            else
                            {
                                var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                if (friendc.ContainsKey(xx.UOwnerID))
                                {
                                    var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                    bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                    if (isfriend)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        e.Destroy();
                                        {
                                            actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                            actualplacer.Inventory.AddItem(e.Name, 1);
                                            actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                        }
                                    }
                                }
                            }
                        }
                        else if (e.Name.ToLower().Contains("sleepingbaga"))
                        {
                            if (!AllowSleepingBags)
                            {
                                if (xx.OwnerID == actualplacer.SteamID)
                                {
                                    break;
                                }
                                else
                                {
                                    var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                    if (friendc.ContainsKey(xx.UOwnerID))
                                    {
                                        var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                        bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                        if (isfriend)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            e.Destroy();
                                            {
                                                actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                                actualplacer.Inventory.AddItem(e.Name, 1);
                                                actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (e.Name.ToLower().Contains("bed"))
                        {
                            if (!AllowBeds)
                            {
                                if (xx.OwnerID == actualplacer.SteamID)
                                {
                                    break;
                                }
                                else
                                {
                                    var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                    if (friendc.ContainsKey(xx.UOwnerID))
                                    {
                                        var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                        bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                        if (isfriend)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            e.Destroy();
                                            {
                                                actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                                actualplacer.Inventory.AddItem(e.Name, 1);
                                                actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (e.Name.ToLower().Contains("barricade"))
                        {
                            if (!AllowBarricade)
                            {
                                if (xx.OwnerID == actualplacer.SteamID)
                                {
                                    break;
                                }
                                else
                                {
                                    var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                    if (friendc.ContainsKey(xx.UOwnerID))
                                    {
                                        var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                        bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                        if (isfriend)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            e.Destroy();
                                            {
                                                actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                                actualplacer.Inventory.AddItem(e.Name, 1);
                                                actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (e.Name.ToLower().Contains("storage"))
                        {
                            if (!AllowBoxes)
                            {
                                if (xx.OwnerID == actualplacer.SteamID)
                                {
                                    break;
                                }
                                else
                                {
                                    var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                    if (friendc.ContainsKey(xx.UOwnerID))
                                    {
                                        var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                        bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                        if (isfriend)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            e.Destroy();
                                            {
                                                actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                                actualplacer.Inventory.AddItem(e.Name, 1);
                                                actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (e.Name.ToLower().Contains("gate"))
                        {
                            if (xx.OwnerID == actualplacer.SteamID)
                            {
                                break;
                            }
                            else
                            {
                                var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                if (friendc.ContainsKey(xx.UOwnerID))
                                {
                                    var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                    bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                    if (isfriend)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        e.Destroy();
                                        {
                                            actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                            actualplacer.Inventory.AddItem(e.Name, 1);
                                            actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                        }
                                    }
                                }
                            }
                        }
                        else if (e.Name.ToLower().Contains("ramp"))
                        {
                            if (!AllowRamps)
                            {
                                if (xx.OwnerID == actualplacer.SteamID)
                                {
                                    break;
                                }
                                else
                                {
                                    var friendc = Fougerite.Server.GetServer().GetRustPPAPI().GetFriendsCommand.GetFriendsLists();
                                    if (friendc.ContainsKey(xx.UOwnerID))
                                    {
                                        var fs = (RustPP.Social.FriendList)friendc[xx.UOwnerID];
                                        bool isfriend = fs.Cast<FriendList.Friend>().Any(friend => friend.GetUserID() == actualplacer.UID);
                                        if (isfriend)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            e.Destroy();
                                            {
                                                actualplacer.MessageFrom(Name, orange + "You are to close to someone else his structure cant build here");
                                                actualplacer.Inventory.AddItem(e.Name, 1);
                                                actualplacer.SendConsoleMessage(aqua + "[ToolCubBoard]" + orange + "You are not allowed to build near this structure");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}                        