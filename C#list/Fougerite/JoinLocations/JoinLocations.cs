using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



// references
using Fougerite;
using Newtonsoft.Json;
using System.Threading;
using System.IO;
using System.Net;


using Module = Fougerite.Module;

namespace JoinLocations
{
    public class JoinLocations : Module
    {
        private IniParser ini;

        Server joinlocations = Server.GetServer();

        public string JoinMessage = "{0} has joined the server {1}";
        public string FailMessage = "{0} has joined the server";
        public string API = "http://ip-api.com/json/";
        string land = string.Empty;

        public bool Smoothmode = false;

        public override void Initialize()
        {
            Fougerite.Hooks.OnPlayerConnected += OnPlayerConnected;

            if(!File.Exists(Path.Combine(ModuleFolder, "JoinLocations.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Joinlocations.ini")).Dispose();
                ini = new IniParser(Path.Combine(ModuleFolder, "Joinlocations.ini"));

                ini.AddSetting("Plugin", "JoinMessage", JoinMessage.ToString());
                ini.AddSetting("Plugin", "FailMessage", FailMessage.ToString());
                ini.AddSetting("Plugin", "API", API.ToString());
                ini.AddSetting("Plugin", "Smoothmode", Smoothmode.ToString());
                ini.Save();
            }
            else
            {
                ini = new IniParser(Path.Combine(ModuleFolder, "Joinlocations.ini"));
                JoinMessage = ini.GetSetting("Plugin", "JoinMessage");
                FailMessage = ini.GetSetting("Plugin", "FailMessage");
                API = ini.GetSetting("Plugin", "API");
                Smoothmode = bool.Parse(ini.GetSetting("Plugin", "Smoothmode"));
            }
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnPlayerConnected -= OnPlayerConnected;
        }
        void OnPlayerConnected(Fougerite.Player netuser)
        {
            if(Smoothmode)
            {
                SmoothMode(netuser);
            }
            else
            {
                GetCounty(netuser);
            }
        }
        void SmoothMode(Fougerite.Player netuser)
        {
            new Thread(() => 
            {
                GetCounty(netuser);
            }).Start();
        }
        void GetCounty(Fougerite.Player netuser)
        {         
            if(API == "")
            {
                var msg = FailMessage.Replace("{0}", netuser.Name);
                joinlocations.Broadcast(msg);
                return;
            }
            County county;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://ip-api.com/json/" + netuser.IP);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                county= JsonConvert.DeserializeObject<County>(json);
            }
            land = county.country;

            string msg1 = JoinMessage.Replace("{0}", netuser.Name).Replace("{1}", land);
            joinlocations.Broadcast(msg1);
        }
    }
    public class County
    {
        public string country { get; set; }
    }
}
    