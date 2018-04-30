using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.Events;
using UnityEngine;

namespace ShootTeleport
{
    public class ShootTeleport : Fougerite.Module
    {
        public override string Name { get { return "ShootTeleport"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Teleports the user to where he shoots"; } }
        public override Version Version { get { return new Version("1.0"); } }
        // using list is much faster then using datastore XD
        private List<string> ShootTP = new List<string> {};
        public override void Initialize()
        {
            Fougerite.Hooks.OnCommand += OnCommand;
            Fougerite.Hooks.OnShoot += OnShoot;
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnCommand -= OnCommand;
            Fougerite.Hooks.OnShoot -= OnShoot;
        }
        public void OnCommand(Fougerite.Player player, string cmd, string[] args)
        {
            if (cmd == "stp")
            {
                if (player.Admin)
                {
                    var id = player.SteamID;
                    if (!ShootTP.Contains(id))
                    {                     
                        ShootTP.Add(id);
                        player.Notice("Shoot teleport enabled");
                    }
                    else
                    {
                        ShootTP.Remove(id);
                        player.Notice("Shoot teleport disabled");
                    }
                }
                else
                {
                    player.Notice("You dont have permissions to use this command");
                }
            }
        }
        public void OnShoot(ShootEvent shootevent)
        {
            var shooter = shootevent.Player;
            var id = shooter.SteamID;
            if (ShootTP.Contains(id))
            {
                // this happens rare but just making sure
                if (shooter != null)
                {
                    RaycastHit[] loc = Physics.RaycastAll(shooter.PlayerClient.controllable.character.eyesRay);
                    if (loc.Count() > 0)
                    {
                        Vector3 goloc = loc[0].point;
                        goloc.y += 2;
                        shooter.TeleportTo(goloc);
                    }
                    else
                    {
                        shooter.Notice("Bullet did not hit shoot again please");
                    }
                }
            }
        }
    }
}
