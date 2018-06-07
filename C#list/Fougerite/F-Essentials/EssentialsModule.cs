using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.Events;
using System.IO;
using System.Net;

namespace F_Essentials
{
    public class EssentialsModule : Module
    {
        public IniParser config;
        public IniParser friends;
        public IniParser share;
        public IniParser HelpList;
        public IniParser RulesList;
        public IniParser SpawnsOverwrite;
        public IniParser PlayerDatabase;


        // config

        public bool JoinMessages;
        public bool LeaveMessage;
        public bool CountryMessage;
        public bool FirstjoinMessage;
       
        public bool EnableRemove;
        
        public bool ShowAirdrop;
        public bool HurtMessages;
        public bool DeathMessages;
        public bool ShowRange;
        public bool OverwriteSpawns;
        public bool EnableHurtPopups;
     
        public bool HomeMoveCheck;

        // tpr settings;
        public bool StructureBlock;
        public bool TprMovecheck;
        public bool SafeTeleport;
        public bool Tokens;
        public bool Enabletpr;
        public int TprCooldown;
        public int TprDelay;

        //home settings
        public bool SleepingHome;
        public bool EnableHome;
        public bool HomeMovecheck;
        public bool StructureHomeBlock;
        public bool CancelHomeOnDamage;
        public bool MaxHomes;
        public int HomeCooldown;
        public int HomeDelay;

        

        public override string Name { get { return "F-Essentials"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "A plugin with all diffrent kinds of commands"; } }
        public override Version Version { get { return new Version("1.0B"); } }

        public List<Player> tprreq = new List<Player>();
        public List<ulong> remove = new List<ulong>();

        public override void Initialize()
        {
            CheckConfig();
            Hooks.OnCommand += Command;
            Hooks.OnAirdropCalled += Airdrop;
            Hooks.OnPlayerConnected += OnPlayerConnected;
            Hooks.OnPlayerDisconnected += OnPlayerDisconnected;
            Hooks.OnChat += Chat;
            Hooks.OnPlayerKilled += Killed;
            Hooks.OnPlayerSpawned += Spawned;
        }
        private void CheckConfig()
        {
            try
            {
                config = new IniParser(Path.Combine(ModuleFolder, "Configuration.config"));

            }
        }
    }
}
