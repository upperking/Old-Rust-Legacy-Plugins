using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WarpsClient
{
    public class GuiClass : MonoBehaviour
    {
        public static bool show = false;
        public Rect startrect = new Rect(200f, 150f, 280f, 200f); // default 460

        private void DoMyWindow(int ID)
        {
            GUI.backgroundColor = Color.cyan;
            GUI.Label(new Rect(10f, 120f, 200f, 20f), "Use Right click on the buttons");
            if (GUI.Button(new Rect(10f, 20f, 180f, 20f), "French Valley"))
            {
                Main.instance.SendMessageToServer("6056,385,-4162");
            }
            if(GUI.Button(new Rect(10f, 40f, 180f, 20f), "Small"))
            {
                Main.instance.SendMessageToServer("6076,376,-3584");
            }
            if(GUI.Button(new Rect(10f, 60f, 180f, 20f), "Big Radtown"))
            {
                Main.instance.SendMessageToServer("5327,368,-4732");
            }
            if(GUI.Button(new Rect(10f, 80f, 180f, 20f), "Next Valley"))
            {
                Main.instance.SendMessageToServer("4668,445,-3908");
            }
            if(GUI.Button(new Rect(10f , 100f, 180f, 20f), "North Hacker Valley"))
            {
                Main.instance.SendMessageToServer("5000, 461, -3000");
            }
            GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
        }
        void OnGUI()
        {
            if (show)
            {
                startrect = GUI.Window(2, startrect, new GUI.WindowFunction(this.DoMyWindow), "Warps");
            }
        }
    }
}
