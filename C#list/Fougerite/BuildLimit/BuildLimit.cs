using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using System.IO;

namespace BuildLimit
{
    public class BuildLimit : Fougerite.Module
    {
        public IniParser ini;

        public int WoodFoundations = 5;
        public int WoodHeight = 10;

        public int MetalFoundations = 5;
        public int MetalHeight = 10;

        public override string Name { get { return "BuildLimit"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "BuildLimit"; } }
        public override Version Version { get {return new Version("1.0"); } }

        public override void Initialize()
        {
            Fougerite.Hooks.OnEntityDeployedWithPlacer += Deploy;

            try
            {
                ini = new IniParser(Path.Combine(ModuleFolder, "Settings.cfg"));
                WoodFoundations = int.Parse(ini.GetSetting("WoodBase", "WoodFoundations"));
                WoodHeight = int.Parse(ini.GetSetting("WoodBase", "WoodHeight")) * 4;
                MetalFoundations = int.Parse(ini.GetSetting("MetalBase", "MetalFoundations"));
                MetalHeight = int.Parse(ini.GetSetting("MetalBase", "MetalHeight")) * 4;
            }
            catch(Exception)
            {
                Logger.LogError("[BuildLimit] Failed to read config its either not found or corrupted");
            }
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnEntityDeployedWithPlacer -= Deploy;
        }
        public void Deploy(Player pl, Entity ent, Player actualplacer)
        {
            if (ent.Name == "WoodPillar")
            {
                try
                {
                    foreach (Entity Ent in ent.GetLinkedStructs())
                    {
                        if (Ent.Name == "WoodFoundation")
                        {
                            if (WoodHeight < ent.Y - Ent.Y)
                            {
                                ent.Destroy();
                                actualplacer.Inventory.AddItem("Wood Pillar", 1);
                                actualplacer.MessageFrom("UberLimit", "You have reached " + WoodHeight / 4 + " max");


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("[BuildLimit] Oops something wrong happed while checking for max wood height");
                    Logger.LogError("Error: " + ex);
                }
            }
            else if (ent.Name == "MetalPillar")
            {
                try
                {
                    foreach (Entity Ent in ent.GetLinkedStructs())
                    {
                        if (Ent.Name == "MetalFoundation")
                        {
                            if (MetalHeight < ent.Y - Ent.Y)
                            {
                                ent.Destroy();
                                actualplacer.Inventory.AddItem("Metal Pillar", 1);
                                actualplacer.MessageFrom("UberLimit", "You have reached " + MetalHeight / 4 + " max");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("[BuildLimit] Oops something wrong happed while checking for max metal height");
                    Logger.LogError("Error: " + ex);
                }
            }
            else if (ent.Name == "WoodFoundation")
            {
                try
                {
                    int amount = 0;

                    foreach (Entity Ent in ent.GetLinkedStructs())
                    {
                        if (Ent.Name == "WoodFoundation")
                        {
                            amount += 1;
                            if (amount == WoodFoundations)
                            {
                                ent.Destroy();
                                actualplacer.MessageFrom("UberLimit", "You have reached the max limit of foundations " + WoodFoundations + " max");
                                actualplacer.Inventory.AddItem("Wood Foundation", 1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("[BuildLimit] Oops something wrong happed while checking for max wood foundations");
                    Logger.LogError("Error: " + ex);
                }
            }
            else if (ent.Name == "MetalFoundation")
            {
                try
                {
                    int amount = 0;

                    foreach (Entity Ent in ent.GetLinkedStructs())
                    {
                        if (Ent.Name == "MetalFoundation")
                        {
                            amount += 1;
                            if (amount == MetalFoundations)
                            {
                                ent.Destroy();
                                actualplacer.MessageFrom("UberLimit", "You have reached the max limit of foundations " + MetalFoundations + " max");
                                actualplacer.Inventory.AddItem("Metal Foundation", 1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("[BuildLimit] Oops something wrong happed while checking for max metal foundations");
                    Logger.LogError("Error: " + ex);
                }
            }
        }
    }
}
