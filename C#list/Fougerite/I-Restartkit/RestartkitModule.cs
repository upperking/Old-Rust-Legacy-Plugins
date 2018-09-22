using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using System.IO;
using User = Fougerite.Player;

namespace I_RestartKit
{
    public class RestartkitModule : Module
    {
        public IniParser users;
        public IniParser ini;

        public string path = Directory.GetCurrentDirectory() + "\\Save\\I-RestartKit\\Users\\";    
        public string defaultpath = Directory.GetCurrentDirectory() + "\\Save\\I-RestartKit\\Kits\\";
        Util util = Util.GetUtil();

        private string aqua = "[color aqua]";

        public override string Name => "i-RestartKit";
        public override string Description => "Allow VIP users (paypal or other) to get restart kit after wipe without any admin intervention. Admin and user friendly plugin";
        public override string Author => "ice cold (Serbian Wolf)";
        public override Version Version => new Version("1.0");

        public string MessageBuy = "Buy your restart kit at www.yourwebsite.com";
        public string MessageBuyChat1 = "You didn't buy a restart kit yet";
        public string MessageBuyChat2 = "To see what are in the kits, type '/restartkit help";
        public string MessageThanks = "Thank for your purchase";
        public string InventoryFull = "You need a Empty inventory";
        public string MessageAlready = "You already received the kit, wait for the next wipe to get one again";
        public string MessageAlready1 = "If you got raided within 24h after the wipe and lost your stuff, ask an admin";


        public override void Initialize()
        {
            Hooks.OnCommand += Command;
            Hooks.OnPlayerConnected += Connect;
            ReadConfig();
            CheckKits();
            
        }
        public void Connect(User player)
        {
            if(!File.Exists(Path.Combine(path, player.SteamID + ".txt")))
            {
                File.Create(Path.Combine(path, player.SteamID + ".txt")).Dispose();
            }
        }
        public void Command(User player, string cmd, string[] args)
        {
            if (cmd == "restartkit")
            {
                if (args.Length == 0)
                {
                    player.MessageFrom(Name, aqua + "Usage /restartkit help");
                    return;
                }
                if (args[0] == "help")
                {
                    player.MessageFrom(Name, aqua + "Usage /restartkit kits = See all avaible kits");
                    player.MessageFrom(Name, aqua + "Usage /restartkit receive kitname - Receive this kit (Only works when the admin gave you acces to it)");
                    player.MessageFrom(Name, aqua + "usage /restartkit info kitname - see the kit items of a specific kit");
                    if(player.Admin)
                    {
                        player.MessageFrom(Name, "/restartkit_adduser player kitname - give the player acces to a kit");
                    }
                }
                else if(args[0] == "info")
                {
                    if (File.Exists(Path.Combine(defaultpath, args[1] + ".txt")))
                    {
                        foreach (string xx in File.ReadAllLines(defaultpath + "\\" + args[1] + ".txt"))
                        {
                            string[] split = xx.Split(':');
                            string one = split[0];
                            string two = split[1];
                            if (one == "Message")
                            {
                                player.MessageFrom(Name, two);
                            }
                            else
                            {
                                player.MessageFrom(Name, $"{aqua}{two}x{one}");
                            }
                        }
                    }
                }
                else if (args[0] == "kits")
                {
                    player.MessageFrom(Name, aqua + "Avaible kits. Usage /restartkit receive kitname");
                    foreach (string file in Directory.GetFiles(defaultpath))
                    {
                        string name = file.Split('\\').Last();
                        player.MessageFrom(Name, $"{aqua} {name.Replace(".txt", string.Empty)}");
                    }
                }
                else if (args[0] == "receive")
                {
                    //util.Log("1");
                    if(!users.ContainsSetting(args[1], player.SteamID)) { player.MessageFrom(Name, aqua + MessageBuyChat1); player.MessageFrom(Name, aqua + MessageBuyChat2); return; }
                    //util.Log("2");
                    if(AlreadyUsedKit(player, args)) { player.MessageFrom(Name, aqua + MessageAlready); player.MessageFrom(Name, aqua + MessageAlready1); return; }
                    if (File.Exists(Path.Combine(defaultpath, args[1] + ".txt")))
                    {
                        if (player.Inventory.FreeSlots == 0)
                        {
                            player.MessageFrom(Name, aqua + InventoryFull);
                            return;
                        }
                       // util.Log("2");
                        foreach (string xx in File.ReadAllLines(defaultpath + "\\" + args[1] + ".txt"))
                        {
                            string[] split = xx.Split(':');
                            string one = split[0];
                          //  util.Log("3");
                            string two = split[1];
                            if (one != "Message")
                            {                            
                                player.Inventory.AddItem(one, Convert.ToInt32(two));                                                                 
                            }
                        }
                        player.MessageFrom(Name, aqua + MessageThanks);
                        StreamWriter writer = File.AppendText(path + player.SteamID + ".txt");
                        writer.WriteLine(args[1]);
                        writer.Close();
                    }
                }
            }
            else if(cmd == "restartkit_adduser")
            {
                if(player.Admin)
                {
                    if (args.Length == 2)
                    {
                        User target = Server.GetServer().FindPlayer(args[0]);
                        users.AddSetting(args[1], target.SteamID, args[1]);
                        users.Save();
                        player.MessageFrom(Name, $"{aqua} You gave {args[1]} kit acces to {target.Name}");
                    }
                }
              
            }
        }

        private bool AlreadyUsedKit(User player, string[] args)
        {
            foreach(string line in File.ReadAllLines(path + "\\" + player.SteamID + ".txt"))
            {
                if(line == args[1])
                {
                    return true;
                }
                return false;
            }
            return false;
               
        }

        private void CheckKits()
        {
            if (!Directory.Exists(defaultpath))
            {
                Directory.CreateDirectory(path);
                util.Log("[I-RestartKit] Created Kits Directory");
            } 
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if(!File.Exists(Path.Combine(defaultpath, "small.txt")))
            {
                StreamWriter writer = new StreamWriter(Path.Combine(path, "small.txt"), true);
                writer.WriteLine("Message:This kit costs 5 dollars");
                writer.WriteLine("Wood Planks:500");
                writer.WriteLine("Leather Helmet:1");
                writer.WriteLine("Leather Vest:1");
                writer.WriteLine("Leather Pants:1");
                writer.WriteLine("Leather Boots:1");
                writer.WriteLine("M4:1");
                writer.WriteLine("556 Ammo:250");
                writer.Close();
                util.Log("[I-RestartKit] Created Restartkit small");
            }
             if(!File.Exists(Path.Combine(defaultpath, "medium.txt")))
             {
                StreamWriter writer2 = new StreamWriter(Path.Combine(path, "medium.txt"), true);
                writer2.WriteLine("Message:This kit costs 10 dollars");
                writer2.WriteLine("Wood Planks:2000");
                writer2.WriteLine("Supply Signal:2");
                writer2.WriteLine("Kevlar Helmet:1");
                writer2.WriteLine("Kevlar Vest:1");
                writer2.WriteLine("Kevlar Pants:1");
                writer2.WriteLine("Kevlar Boots:1");
                writer2.WriteLine("Research Kit:2");
                writer2.WriteLine("M4:1");
                writer2.WriteLine("Bolt Action Rifle:1");
                writer2.WriteLine("556 Ammo:400");
                writer2.Close();
                util.Log("[I-RestartKit] Created Restartkit medium");
            }
            if(!File.Exists(Path.Combine(defaultpath, "special.txt")))
            {
                StreamWriter writer3 = new StreamWriter(Path.Combine(path, "special.txt"), true);
                writer3.WriteLine("Message:This kit costs 20 dollars");
                writer3.WriteLine("Wood Planks:4000");
                writer3.WriteLine("Supply Signal:5");
                writer3.WriteLine("Kevlar Helmet:1");
                writer3.WriteLine("Kevlar Vest:1");
                writer3.WriteLine("Kevlar Pants:1");
                writer3.WriteLine("Kevlar Boots:1");
                writer3.WriteLine("Research Kit:5");
                writer3.WriteLine("M4:1");
                writer3.WriteLine("Bolt Action Rifle:1");
                writer3.WriteLine("556 Ammo:600");
                writer3.Close();
                util.Log("[I-RestartKit] Created Restartkit special");
            }
          
        }

        private void ReadConfig()
        {
            if(!File.Exists(Path.Combine(ModuleFolder, "Settings.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Settings.ini")).Dispose();
                ini = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                ini.AddSetting("Config", "MessageBuy", MessageBuy);
                ini.AddSetting("Config", "MessageBuyChat1", MessageBuyChat1);
                ini.AddSetting("Config", "MessageBuyChat2", MessageBuyChat2);
                ini.AddSetting("Config", "MessageThanks", MessageThanks);
                ini.AddSetting("Config", "InventoryFull", InventoryFull);
                ini.AddSetting("Config", "MessageAlready", MessageAlready);
                ini.AddSetting("Config", "MessageAlready1", MessageAlready1);
                ini.Save();
            }
            else
            {
                ini = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                MessageBuy = ini.GetSetting("Config", "MessageBuy");
                MessageBuyChat1 = ini.GetSetting("Config", "MessageBuyChat1");
                MessageBuyChat2 = ini.GetSetting("Config", "MessageBuyChat2");
                MessageThanks = ini.GetSetting("Config", "MessageThanks");
                InventoryFull = ini.GetSetting("Config", "InventoryFull");
                MessageAlready = ini.GetSetting("Config", "MessageAlready");
                MessageAlready1 = ini.GetSetting("Config", "MessageAlready1");
            } 
            if(!File.Exists(Path.Combine(ModuleFolder, "users.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "users.ini")).Dispose();
                users = new IniParser(Path.Combine(ModuleFolder, "users.ini"));
            }
            else
            {
                users = new IniParser(Path.Combine(ModuleFolder, "users.ini"));
            }
        }

        public override void DeInitialize()
        {
            Hooks.OnCommand -= Command;
        }
    }
}
