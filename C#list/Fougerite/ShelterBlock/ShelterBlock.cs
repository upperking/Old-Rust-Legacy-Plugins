using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;

namespace ShelterBlock
{
    public class ShelterBlock : Fougerite.Module
    {
        public override string Name { get { return "ShelterBlock"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "ShelterBlock"; } }
        public override Version Version { get { return new Version("1.2"); } }

        public override void Initialize()
        {
            Fougerite.Hooks.OnEntityDeployedWithPlacer += OnEntityDeployed;
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnEntityDeployedWithPlacer += OnEntityDeployed;
        }
        public void OnEntityDeployed(Fougerite.Player pl, Fougerite.Entity e, Fougerite.Player actualplacer)
        {
            if (!e.Name.ToLower().Contains("Wood shelter"))
            {
                e.Destroy();
                {
                    pl.MessageFrom("ShelterBlocker", "You cannot place shelters on this server");
                    pl.Notice("✘", "Shelters are forbidden");
                    return;
                }
            }
        }
    }
}





