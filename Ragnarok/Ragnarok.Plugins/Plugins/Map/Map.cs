using System;
using UnityEngine;
using System.IO;

namespace Ragnarok.Plugins.Map
{
    /// <summary>
    /// Map by gintaras
    /// </summary>
    public class Map : MonoBehaviour
    {
        public bool ShowMap = false;
        public bool ShowMiniMap = false;
        public Texture2D imageMap;
        public Texture2D ButtonOFF;
        public Texture2D ButtonHover;
        public Texture2D ButtonON;
        public Texture2D Taskelis;
        public void Start()
        {
            byte[] bytes = File.ReadAllBytes(Path.Combine(RGuard.RGuard.instance.datadir + "Logos", "Map.png"));
            byte[] BButtonOFF = System.Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQECAgICAgICAgICAgMDAwMDAwMDAwP/2wBDAQEBAQEBAQIBAQICAgECAgMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwP/wAARCAAQADwDAREAAhEBAxEB/8QAGgAAAQUBAAAAAAAAAAAAAAAABQIGCAkKB//EADAQAAAGAQIDBQgDAQAAAAAAAAIDBAUGBwgBEwAJEhEUFRYhIiQxMjNUpNNTVWEK/8QAFAEBAAAAAAAAAAAAAAAAAAAAAP/EABQRAQAAAAAAAAAAAAAAAAAAAAD/2gAMAwEAAhEDEQA/AMjD29uaRzUp06nbJL2egGynH2dacoYvaGUIWvaIWuvrrwArzI9fe/jpP0cBq6mH/Py3yugeUvb+OdiWs/OeZSPHVTlg2SxdAXtLVLLd9RNNru1kQJKwwOOOEdg8Qb43KwCA8DehnCChB3jQRSgR4c1vLkv1Yw86fHnlxUlYl0vlGWPWkOt+xp9Knau3iwGOC7E5fp4vZJAx1pHog3APYYgUlaT1bMrLKcVxWpoVARaFcAhy5OdDp+dJi5g4y25cspwpy4pNRfNSW+0Otea2Y+QjTH2zbASHt001rE2AOfeZ/W4jOoMcLFowryC9S9DhBWDCN2aXL5oOvuX8lzrxtl10RYEZzGsbEWwaqvqa1tYyh2XRB0maNomtdzGD05TOijv6GLkKVTUpaTRpwqVGgFIwodTFIUR+ZHr738dJ+jgHV4kt8seI73vn822V/YbH09va+l6fL/vx4AU9sjmrc1KhOm3CTNnoHvJwdvQnKAL2RmhFp2CDrp66cAK8tvX2X5CT9/AaR7V57tpRKhaGpfDqOvMaAi5ZNS4KZDuFvsDYWYnmdfRVbGjrJopXCLYU6Ermsl+cwtLm9pQjCBV1GtgTNA9ASJsDnhYwiydyhzDqqpL9S3lK+XhCcQMX3CdQWmHRghFh6K5gun81sBjVW9JmI2OErAx0baEKB8MXlFLkytIQnMEBUDWoDnbUIhfuU/cmS1S2utyD5fy7JWDWE6UXVdAwut5ZRlt05Ztd12y1xCI5YFXxNgdoSsXxZONrKZGZqTISHE9Kd1iLSHBSDmVmvkpl+4iiUslOxQMNseyZnTlPRqB1FTkKiYJ5LH2QnPjrA6ZYIrEXiwVxb2cJe8rQOTocoVKtRLDO8HDNCC3lt6+y/ISfv4B1eGrfLHh2z75/DuFf2G/9Tc2vpevzf58eA//Z");
            byte[] BButtonHover = System.Convert.FromBase64String("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");
            byte[] BButtonON = System.Convert.FromBase64String("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");
            byte[] BTaskelis = System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAA8AAAAaCAYAAABozQZiAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAABGhJREFUeNpsVFtMHGUU/v657czsjb0Bu9xBsQkkBkrpg1apGCMPjUYTEyX6sj740FTfGn3xwbcaYqJNDKY2MTGNmjTWS0nTlNSSXmkFRC4NlNAWWHZh2fvsdXbWs2htFzgPM/nPnO8/l+98w9DQCKSzMLndyEWjwEbo9Wdrve/1qvwBKwxbThCit8KxkTu6MIJ89mJzT08+sRYAYyWwR2BUOevbYuGTx7321wYbq6EKIsAJQEGHwQz8FNzCZxMLf6w833dUWF2Z5bANbiBwoa4rp10Y6W3vrK1xI6tlYBCIZxyKpRJKPAezWcZWaBP+dS3wSwYvukR2jy4QUKtnh37ubu2sdTsQ2oxDNkpQzSp0SYCqmmBmQGgrAVe1B8M+1dcaeng6JUqMF6sbek7Y8Xn/0038RiQFlyohZACfzixhaOEBJqJJdLqqUGOSsJlMo8blQDMrNp3NYol1trefulxv8VdZrNCLRWgcw8tjU5iKJ/HIOqwWjPZ1oYoqArXC5TS8shy7xHXr6f1uWUG2UIQsm3B6ab0CWLbZZArDi6swKSZkCgWIioqDorGP8/GcDdRbqUS1MoaJRAp72WSc/JSYKx9ECR4GM/egUExSveDKXsPAYY9jT3Dff36j/ND18lw0btxkHl+Lx6FKEpLE9/sttXiV6HrS+t1OHG3zQUtnoNDgkskkrhT4Sd7Z3x9m8/P+F3weJHI6ZAp+u6EGXkVBtSjC3+LDUGcrWNFAPK/DblXw4/IqvhVdx1nLwBFEbt46Ndnh9bfUuBCMa7BJIlQaznaTtEfpbG77Yo9FQS6bxr4b8xdXqhsHeIfFiaCsjC2ur7076HXaGMdDo4lmcgViQIeWzSNPWRWRo5IFfDi1FBvNi2+AK4U5ibI4VDk2EtY+GJpbpjWUwFM2Rnxv56U3aQA2qxlnFu/j6/XER0wxLRAx4I0qFzL5HHRg4Uok7h60mnq9Dge0fB4cRRSJAYdFRZiGemQucC5td3ws8Qw8LRSftdmgUwCjLdJV++3NROqdN52yjTEeBRKFLAhQKPUnd1dyly2etwSGMEd+RtwKh1/q+5cPKo2328JXr9048edK8Mv9TfXQ6HYr9Xp3dR2/2mvOdOw/MM9rj7dPWJ+d+f8gyTLWHj787pti+vhwe3OdkCGdF3WcTRfyenvzF2ajgJIoPAbfvX595zIlfnc5LiyF4/5mhw2h4AbOl+RLTov6t17+05Qn9Qjc9tyhCqRBH1N68fvzoaD/WJ0Lo4kMVpz1P6iRGLiyqp4w4WB3b4WjPIhgOjN2dezC7LGNzY5xSBu21qfOl/VcMoxK8J252UowZd4ySoZXlH+LRbWO6bwxcn9mOhIVy8rbkdlkkSs7pu8eEolHbb528q+biJjdY3w2hbxWlmxlqIBYbJf8JBJEIBpZ/ioQQdfAoQfP+OqgZzO74oTVqand4qXyeFEsJBVzbHp6QrMu30ORON8FNqnmPcVPq5m0cvy5rWAwFg4Etmex0/4RYAChB+LNgR7pMQAAAABJRU5ErkJggg==");
            imageMap = new Texture2D(700, 558, TextureFormat.RGB24, false);
            ButtonOFF = new Texture2D(60, 16, TextureFormat.RGB24, false);
            ButtonHover = new Texture2D(60, 16, TextureFormat.RGB24, false);
            ButtonON = new Texture2D(60, 16, TextureFormat.RGB24, false);
            Taskelis = new Texture2D(15, 26, TextureFormat.RGBA32, false);

            imageMap.LoadImage(bytes);
            ButtonOFF.LoadImage(BButtonOFF);
            ButtonHover.LoadImage(BButtonHover);
            ButtonON.LoadImage(BButtonON);
            Taskelis.LoadImage(BTaskelis);

            
        }
        public void Update()
        {
           if (Input.GetKeyDown(KeyCode.M) && !ConsoleWindow.IsVisible() && !ChatUI.IsVisible() && !MainMenu.IsVisible())
            {
                if (ShowMap)
                {
                    ShowMap = false;
                }
                else
                {
                    ShowMap = true;
                }
            }
        }
        public void OnGUI()
        {           
            if(!NetCull.isClientRunning)
            {
                return;
            }
            int w = Screen.width, h = Screen.height;
            Rect Mikpos = new Rect(65, 0, 60, 16);
            Rect minipos = new Rect(130, 0, 60, 16);
            if (Mikpos.Contains(Event.current.mousePosition))
            {
                GUI.DrawTexture(Mikpos, ButtonHover, ScaleMode.StretchToFill);
                if (Event.current.type == EventType.MouseDown || Input.GetButtonDown("Fire1"))
                {
                    if (ShowMap)
                    {
                        ShowMap = false;
                    }
                    else
                    {
                        ShowMap = true;
                    }
                }
            }
            else
            {
                if (ShowMap)
                {
                    GUI.DrawTexture(Mikpos, ButtonON, ScaleMode.StretchToFill);
                }
                else
                {
                    GUI.DrawTexture(Mikpos, ButtonOFF, ScaleMode.StretchToFill);
                }
            }
           
            if (minipos.Contains(Event.current.mousePosition))
            {
                GUI.DrawTexture(minipos, ButtonHover, ScaleMode.StretchToFill);
                if (Event.current.type == EventType.MouseDown || Input.GetButtonDown("Fire1"))
                {
                    if (ShowMiniMap)
                    {
                        ShowMiniMap = false;
                    }
                    else
                    {
                        ShowMiniMap = true;
                    }
                }
            }
            else
            {
                if (ShowMiniMap)
                {
                    GUI.DrawTexture(minipos, ButtonON, ScaleMode.StretchToFill);
                }
                else
                {
                    GUI.DrawTexture(minipos, ButtonOFF, ScaleMode.StretchToFill);
                }
            }
            if (ShowMap)
            {
                GUI.DrawTexture(new Rect(w / 2 - 350, h / 2 - 279, 700, 558), imageMap);
                Transform PlayerTrans = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>().transform;
                float xkord = PlayerTrans.position.x;
                float zkord = PlayerTrans.position.z * -1.0000f;
                if (xkord > 4000f && xkord < 7500f && zkord > 1500f && zkord < 6150f)
                {
                    int taskokordx = (int)((float)h / 2.000f - 279.000f + (558.000f / 3500.000f * (xkord - 4000.000f)));
                    int taskokordz = (int)((float)w / 2.00f - 350.00f + (700.00f - 700.00f / 4650.00f * ((float)zkord - 1500f)));
                    GUI.DrawTexture(new Rect((taskokordz - 7), (taskokordx - 26), 15, 26), Taskelis);
                }
                else
                {
                    GUIStyle style = new GUIStyle();
                    Rect rect = new Rect(w / 2 - 350, h / 2 - 279, w / 10, h * 2 / 100);
                    style.alignment = TextAnchor.UpperLeft;
                    style.fontSize = h * 2 / 100;
                    style.normal.textColor = Color.white;
                    style.richText = true;
                    string text = "<b>Out of zone</b>";
                    GUI.Label(rect, text, style);
                }
            }
            if (ShowMiniMap)
            {
                Transform PlayerTrans = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>().transform;
                float xkord = PlayerTrans.position.x;
                float zkord = PlayerTrans.position.z * -1.0000f;
                if (xkord > 4000f && xkord < 7500f && zkord > 1500f && zkord < 6150f)
                {
                    int taskokordx = (int)((float)((558.000f / 3500.000f * (xkord - 4000.000f) - 75.00f) * -1.00f));
                    int taskokordz = (int)((float)(((700.00f - 700.00f / 4650.00f * ((float)zkord - 1500f)) - 75.00f) * -1.00f));
                    GUI.BeginGroup(new Rect(65, 20, 150, 150));
                    GUI.DrawTexture(new Rect(taskokordz, taskokordx, 700, 558), imageMap);
                    GUI.DrawTexture(new Rect(68, 49, 15, 26), Taskelis);
                    GUI.EndGroup();
                }
                else
                {
                    GUIStyle style = new GUIStyle();
                    Rect rect = new Rect(65, 20, w / 10, h * 2 / 100);
                    style.alignment = TextAnchor.UpperLeft;
                    style.fontSize = h * 2 / 100;
                    style.normal.textColor = Color.white;
                    style.richText = true;
                    string text = "<b>Out of zone</b>";
                    GUI.Label(rect, text, style);
                }
            }
        }
    }
}