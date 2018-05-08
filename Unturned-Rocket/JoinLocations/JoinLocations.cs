using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
namespace JoinLocations
{
    public class JoinLocations : RocketPlugin<JoinLocationsConfig>
    {
        string land = string.Empty;
        public JoinLocations Instance;

        protected override void Load()
        {
            Instance = this;
            Configuration.Save();
            U.Events.OnPlayerConnected += Connected;
        }
        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Connected;
        }
        void Connected(UnturnedPlayer player)
        {
            if(this.Configuration.Instance.SmoothMode)
            {
                SmoothMethod(player);
                return;
            }
            else
            {
                GetCountry(player);
            }
        }
        void SmoothMethod(UnturnedPlayer player)
        {
            new Thread(() =>
            {
                GetCountry(player);
            }).Start();
        }
        void GetCountry(UnturnedPlayer player)
        {
            County county;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://ip-api.com/json/" + player.IP);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                county = JsonConvert.DeserializeObject<County>(json);
            }
            land = county.country;
            var message = this.Configuration.Instance.JoinMessage;
            string msg = message.Replace("{0}", player.DisplayName).Replace("{1}", land);
            UnturnedChat.Say(msg);

        }
    }
    public class County
    {
        public string country { get; set; }
    }
}
