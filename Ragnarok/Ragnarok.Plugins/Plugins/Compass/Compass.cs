using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ragnarok.Plugins.Compass
{
    public class Compass : MonoBehaviour
    {
        public string direction = string.Empty;
        public int wi = 0;
        public void Start()
        {
          //  this.StartCoroutine(GetDirection());
        }

        private void Update()
        {
            wi = Screen.width;
            try
            {
                Character character = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
                float rot = character.eyesRotation.eulerAngles.y;
                if (rot > 337.5 || rot < 22.5)
                    direction = "North";

                else if (rot > 22.5 && rot < 67.5)
                    direction = "North-East";

                else if (rot > 67.5 && rot < 112.5)
                    direction = "East";

                else if (rot > 112.5 && rot < 157.5)
                    direction = "South-East";

                else if (rot > 157.5 && rot < 202.5)
                    direction = "South";

                else if (rot > 202.5 && rot < 247.5)
                    direction = "South-West";

                else if (rot > 247.5 && rot < 292.5)
                    direction = "West";

                else if (rot > 292.5 && rot < 337.5)
                    direction = "North-West";
            }
            catch { }
          
        }        
        public void OnGUI()
        {
            if(!string.IsNullOrEmpty(direction))
            {
                int w = Screen.width, h = Screen.height;
                GUIStyle style = new GUIStyle();
                style.fontSize = h * 2 / 125;
                style.richText = true;
                style.fontStyle = FontStyle.Bold;
                GUI.Label(new Rect((float)(wi / 2 - 45), 0f, 90f, 18f), "<color=#ff8c00>" + direction + "</color>", style);
            }
        }
    }
}
