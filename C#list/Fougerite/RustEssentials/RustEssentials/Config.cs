using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RustEssentials
{
    public class Config
    {
        public static Dictionary<string, string> defaultsettings = new Dictionary<string, string>()
        {
            {"InvalidCommand", "Unknown command {0}" },
            {"SaveTime", "10" },
            {"SystemColor", "[color #76ee00]" },
            {"NoAccess", "You dont have acces to this command" },
            {"AllowCracked", "true" }
        };
        public static Dictionary<string, string> warp = new Dictionary<string, string>()
        {
            {"EnableWarps", "true" },
            {"UseMoney", "false" },
            {"Money", "100" },
            {"NotEnoughMoney", "You can only warp for {0}" },
            {"NoAccesWarp", "You dont have acces to use warp {0}" },
            {"Warping", "You wil be warped to {0} in {1} Seconds"},
            {"Warped", "You have been warped to {0}"},
            {"WarpSet", "Warp {0} saved at {1}"},
            {"WarpRemove", "Warp {0} removed" }
        };

    }
}
