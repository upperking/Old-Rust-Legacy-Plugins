using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Fougerite;
using Newtonsoft.Json;
using System.Net;
namespace VPNBlocker
{
    public class Class : Fougerite.Module
    {
        public override string Name { get { return "VPNBlocker"; } }
        public override string Author { get { return "ice cold"; } }
        public override string Description { get { return "Blocks people from joining when using vpn/proxy/isp"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public override void Initialize()
        {
            Fougerite.Hooks.OnPlayerConnected += OnPlayerConnected;
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnPlayerConnected -= OnPlayerConnected;
        }
        void OnPlayerConnected(Fougerite.Player player)
        {
            if (player.Admin)
            {
                return;
            }
            string ip = player.IP;

            var url = string.Format("http://legacy.iphub.info/api.php?ip=" + ip + "&showtype=4");
            Web.GET(url, (code, response) =>
            {
                if (code != 200 || string.IsNullOrEmpty(response))
                {
                    Fougerite.Logger.LogError("Service temporarily offline");
                }
                else
                {
                    var jsonresponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                    var playervpn = (jsonresponse["proxy"].ToString());
                    var playerispvpn = (jsonresponse["asn"].ToString());
                    {
                        if (playervpn == "1")
                        {
                            player.Disconnect();
                            Fougerite.Logger.LogWarning(player.Name + "| " + player.SteamID + " | " + player.IP + " is using vpn KICKED!!");
                        }
                    }
                }
            });
        }
    }
}
