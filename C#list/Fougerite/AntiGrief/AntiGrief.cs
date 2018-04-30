using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using System.IO;

namespace AntiGrief
{
    public class AntiGrief : Fougerite.Module
    {
        private IniParser Settings;
        public bool BlockShelterCloseToDoor;
        public bool BlockSpikeCloseToDoor;
        public bool BlockBarricadeCloseToDoor;
        public bool AntiGatewayStag;
        private string orange = "[color orange]";
        private string sys = "I-AntiGrief";
        public override string Name { get { return "AntiGrief"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Prevents people from placing shelters at DoorWays"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public override void Initialize()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "Settings.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Settings.ini")).Dispose();
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                Settings.AddSetting("Settings", "BlockShelterCloseToDoor", "true");
                Settings.AddSetting("Settings", "BlockSpikeCloseToDoor", "true");
                Settings.AddSetting("Settings", "BlockBarricadeCloseToDoor", "true");
                Settings.AddSetting("Settings", "AntiGatewayStag", "true");
                Settings.Save();
            }
            else
            {
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                BlockShelterCloseToDoor = bool.Parse(Settings.GetSetting("Settings", "BlockShelterCloseToDoor"));
                BlockSpikeCloseToDoor = bool.Parse(Settings.GetSetting("Settings", "BlockSpikeCloseToDoor"));
                BlockBarricadeCloseToDoor = bool.Parse(Settings.GetSetting("Settings", "BlockBarricadeCloseToDoor"));
                AntiGatewayStag = bool.Parse(Settings.GetSetting("Settings", "AntiGatewayStag"));
            }
            ReloadSettings();
            Fougerite.Hooks.OnEntityDeployedWithPlacer += OnEntityDeployed;
            Fougerite.Hooks.OnCommand += OnCommand;
            Logger.Log("Yes I-Antigrief loaded");
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnEntityDeployedWithPlacer -= OnEntityDeployed;
            Fougerite.Hooks.OnCommand -= OnCommand;
            Logger.Log("antigrief unloaded");
        }
        public void OnEntityDeployed(Fougerite.Player pl, Fougerite.Entity e, Fougerite.Player actualplacer)
        {
            if (e.Name == "Wood_Shelter")
            {
                if (BlockShelterCloseToDoor)
                {
                    foreach (Fougerite.Entity xx in Util.GetUtil().FindEntitiesAround(e.Location, 5f))
                    {
                        if (xx.Name == "WoodDoorFrame")
                        {
                            e.Destroy();
                            pl.MessageFrom(sys, orange + "You cannot place Wood Shelter so close to DoorWays");
                        }
                        else if (xx.Name == "MetalDoorFrame")
                        {
                            e.Destroy();
                            pl.MessageFrom(sys, orange + "You cannot place Wood Shelter so close to DoorWays");
                        }
                    }
                }
            }
            else if (e.Name.ToLower().Contains("spike"))
            {
                if (BlockSpikeCloseToDoor)
                {
                    foreach (Fougerite.Entity xx in Util.GetUtil().FindEntitiesAround(e.Location, 5f))
                    {
                        if (xx.Name == "WoodDoorFrame")
                        {
                            if (xx.OwnerID == pl.SteamID)
                            {
                                return;
                            }
                            else
                            {
                                e.Destroy();
                                pl.MessageFrom(sys, orange + "You cannot place " + e.Name + " so close to doorways");
                            }
                        }
                        else if (xx.Name == "MetalDoorFrame")
                        {
                            if (xx.OwnerID == pl.SteamID)
                            {
                                return;
                            }
                            else
                            {
                                e.Destroy();
                                pl.MessageFrom(sys, orange + "You cannot place " + e.Name + " so close to doorways");
                            }
                        }
                    }
                }
            }
            else if (e.Name.ToLower().Contains("barricade"))
            {
                if (BlockBarricadeCloseToDoor)
                {
                    foreach (Fougerite.Entity xx in Util.GetUtil().FindEntitiesAround(e.Location, 5f))
                    {
                        if (xx.Name == "WoodDoorFrame")
                        {
                            if (xx.OwnerID == pl.SteamID)
                            {
                                return;
                            }
                            else
                            {
                                e.Destroy();
                                pl.MessageFrom(sys, orange + "You cannot place " + e.Name + " so close to doorways");
                            }
                        }
                        else if (xx.Name == "MetalDoorFrame")
                        {
                            if (xx.OwnerID == pl.SteamID)
                            {
                                return;
                            }
                            else
                            {
                                e.Destroy();
                                pl.MessageFrom(sys, orange + "You cannot place " + e.Name + " so close to doorways");
                            }
                        }
                    }
                }
                else if (e.Name == "WoodGate")
                {
                    if (AntiGatewayStag)
                    {
                        foreach (Fougerite.Entity xx in Util.GetUtil().FindEntitiesAround(e.Location, 3f))
                        {
                            if (xx.Name == "WoodGate")
                            {                                
                                e.Destroy();
                                {
                                    pl.MessageFrom(sys, orange + "You cannot double the Gates on this server :)");                              
                                }
                            }
                        }
                    }
                }
            }
        }
        public void OnCommand(Fougerite.Player pl, string cmd, string[] args)
        {
            if (cmd == "antigrief")
            {
                if (pl.Admin)
                {
                    pl.MessageFrom(sys, orange + "I-AntiGrief by ice cold");
                    pl.MessageFrom(sys, orange + "Settings Reloaded");
                    ReloadSettings();
                }
                else
                {
                    pl.MessageFrom(sys, "You dont have acces this command");
                }
            }
        }
        private void ReloadSettings()
        {
            Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
            {
                BlockShelterCloseToDoor = bool.Parse(Settings.GetSetting("Settings", "BlockShelterCloseToDoor"));
                BlockSpikeCloseToDoor = bool.Parse(Settings.GetSetting("Settings", "BlockSpikeCloseToDoor"));
                BlockBarricadeCloseToDoor = bool.Parse(Settings.GetSetting("Settings", "BlockBarricadeCloseToDoor"));
            }
        }
    }
}
                          
                         
                 