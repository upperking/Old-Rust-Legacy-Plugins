using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RustBuster2016.API;

namespace WarpsClient
{
    public class Main : RustBusterPlugin
    {
        public override string Name => "WarpsClient";
        public override string Author => "ice cold";
        public override Version Version => new Version("1.0");

        public GameObject go;
        public static Main instance;

        public override void Initialize()
        {
            instance = this;
            Hooks.OnRustBusterClientConsole += console;
            if(IsConnectedToAServer)
            {
                go = new GameObject();
                go.AddComponent<GuiClass>();
                //go.AddComponent<RpcCatcher>();
                UnityEngine.Object.DontDestroyOnLoad(go);
            }
         
        }
        public override void DeInitialize()
        {
            Hooks.OnRustBusterClientConsole -= console;
            if (go != null)
            {
                UnityEngine.Object.DestroyImmediate(go);
            }
            else
            {
                Debug.Log("[color red]Failed to destroy a GameObject on [WarpsClient]");
            }
        }

        void console(string msg)
        {
            if(msg == "turnonwarpclientgui")
            {
                if(GuiClass.show)
                {
                    GuiClass.show = false;
                    Rust.Notice.Popup("!", "Warp GUI disabled", 5f);
                }
                else
                {
                    GuiClass.show = true;
                    Rust.Notice.Popup("!", "Warp GUI Enabled", 5f);
                }
            }
        }
    }
}
