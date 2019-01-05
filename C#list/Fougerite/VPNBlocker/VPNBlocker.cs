using System;
using Fougerite;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace VPNBlocker
{
    public class VPNBlocker : Module
    {
        public XmlDocument Document { get; set; }
        public override string Name => "VPNBlocker";
        public override string Author => "ice cold";
        public override string Description => "Checks for Player VPN";
        public override Version Version => new Version("1.0");

        public bool iphub = false;

        public string iphubkey = "X-Key: KEY HERE";






        public override void DeInitialize()
        {
            Hooks.OnPlayerConnected -= Connect;
        }

        public override void Initialize()
        {
            Hooks.OnPlayerConnected += Connect;

            CheckConfig();


        }

        private void Connect(Player player)
        {
            if (iphub)
            {
                CheckIpHUBVpn(player);
            }
        }

        public void CheckIpHUBVpn(Player player)
        {

            try
            {
                if (player != null)
                {
                    string url = "http://v2.api.iphub.info/ip/" + player.IP;
                    Detector vpn;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Headers.Add(iphubkey);
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        int code = (int)response.StatusCode;
                        if(code == 429)
                        {
                            Logger.Log("You have reached the limit of allows requested per day");
                            return;
                        }
                        var json = reader.ReadToEnd();
                        vpn = JsonConvert.DeserializeObject<Detector>(json);
                    }
                    if (vpn.block == 1)
                    {
                        player.Disconnect();
                        Logger.Log("[AntiVPN] " + player.Name + " has been kicked for using VPN");
                    }

                }


            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }
        public void CheckConfig()
        {
            if (!File.Exists(Path.Combine(ModuleFolder, "Config.xml")))
            {
                Document = new XmlDocument();
                XmlDeclaration declaration = Document.CreateXmlDeclaration("1.0", "utf-8", "yes");
                XmlComment comment = Document.CreateComment("VPNBlocker Configuration");
                Document.InsertBefore(declaration, Document.DocumentElement);
                Document.InsertAfter(comment, declaration);

                XmlElement element = Document.CreateElement("Configuration");
                Document.AppendChild(element);

             
                XmlElement element2 = Document.CreateElement("bool");
                element2.SetAttribute("iphub", iphub.ToString());
                element.AppendChild(element2);

                XmlElement element3 = Document.CreateElement("string");
                element3.SetAttribute("iphubkey", iphubkey);
                element.AppendChild(element3);

                Document.Save(Path.Combine(ModuleFolder, "Config.xml"));
            }
            else
            {
                iphub = Convert.ToBoolean(GetSetting("iphub"));
                iphubkey = GetSetting("iphubkey");

                Logger.Log("iphubkey = " + iphubkey);
            }
        }
        public string GetSetting(string attribute)
        {
            Document = new XmlDocument();
            Document.Load(Path.Combine(ModuleFolder, "Config.xml"));
            XmlNode Node = Document.GetElementsByTagName("Configuration").Item(0);

            if (Node != null && Node.HasChildNodes)
            {
                foreach (XmlElement child in Node.ChildNodes)
                {
                    if(child.HasAttribute(attribute))
                    {
                        return child.GetAttribute(attribute);
                    }
                }
            }
            return null;
        }
    }
    public class Detector
    {
        public int block { get; set; }

    }

}
