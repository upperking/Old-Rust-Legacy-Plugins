using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;

namespace JoinLocations
{
    public class JoinLocationsConfig : IRocketPluginConfiguration
    {
        public string JoinMessage = "{0} has joined the server from {1}";
        public bool SmoothMode = false;

        public void LoadDefaults() { }

    }
}
