using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Rocket.API;

using Rocket.API.Collections;
using System.IO;
namespace JoinLocations
{
    public class JoinLocations : RocketPlugin<JoinLocationsConfig>
    {
        string land = string.Empty;
        public static JoinLocations Instance;

        protected override void Load()
        {
            JoinLocations.Instance = this;
            Configuration.Save();
            U.Events.OnPlayerConnected += Connected;
        }
        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Connected;
        }
        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"JoinMessage", "{Player} has joined from {Country}" },
                    {"FailMessage", "{Player} has joined from Unknown" }
                };
            }
        }

        public void Connected(UnturnedPlayer player)
        {
            GetCountry(player);
        }
        public void GetCountry(UnturnedPlayer player)
        {
            try
            {
                Country county;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://ip-api.com/json/" + player.IP);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    county = JsonConvert.DeserializeObject<Country>(json);
                }
                land = county.country;
                var message = JoinLocations.Instance.Translate("JoinMessage");
                string msg = message.Replace("{Player}", player.DisplayName).Replace("{Country}", land);
                UnturnedChat.Say(msg);
            }
            catch
            {
                var message = JoinLocations.Instance.Translate("FailMessage");
                string msg = message.Replace("{Player}", player.DisplayName);
                UnturnedChat.Say(msg);
            }
           

        }
    }
}
