using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;

namespace Teleporter
{
    public class Configuration : IRocketPluginConfiguration
    {
        public int TprDelay, TprCooldown;

        public void LoadDefaults()
        {
            this.TprDelay = 20;
            this.TprCooldown = 60;
        }
        
    }
}
