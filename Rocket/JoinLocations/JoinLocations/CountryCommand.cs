using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using Rocket.API;
using UnityEngine;
using System.Net;
using Newtonsoft.Json;
using System.IO;


namespace JoinLocations
{
    public class CountryCommand : IRocketCommand
    {
        public string land = string.Empty;
        public List<string> Permissions
        {
            get
            {
                return new List<string>()
                {
                    "JoinLocations.CountryCommand"
                };
            }
        }

        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Both;
            }
        }

        public string Name
        {
            get
            {
                return "country";
            }
        }
        public string Syntax
        {
            get
            {
                return "country Player";
            }
        }
        public string Help
        {
            get
            {
                return "Usage /country Player";
            }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>();
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, "Usage /country Player", Color.blue);
                return;
            }
            UnturnedPlayer pl = UnturnedPlayer.FromName(command[0]);
            try
            {
                Country county;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://ip-api.com/json/" + pl.IP);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    county = JsonConvert.DeserializeObject<Country>(json);
                }
                land = county.country;
                var message = JoinLocations.Instance.Translate("ViewMessage");
                string msg = message.Replace("{Player}", pl.DisplayName).Replace("{Country}", land);
                UnturnedChat.Say(caller, msg, Color.green);
            }
            catch
            {
                UnturnedChat.Say(caller, "Something went wrong please try again", Color.red);

            }
        }
    }
}
