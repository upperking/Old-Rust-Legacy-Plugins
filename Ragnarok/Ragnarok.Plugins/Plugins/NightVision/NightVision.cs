using System;
using UnityEngine;
using RGuard;
using System.IO;
namespace Ragnarok.Plugins.Plugins.NightVision
{
    public class NightVision :  MonoBehaviour
    {
        static GameObject lightobject;
        static new Light light;

        static float countdown = 30f;
        static Texture2D image;

        static bool isrunning;
        public void Start()
        {
            isrunning = false;
            byte[] imagebyte = File.ReadAllBytes(RGuard.RGuard.instance.datadir + "Images\\NightVisionGoggles.png");
            image = new Texture2D(40, 40, TextureFormat.RGBA32, false);
            image.LoadImage(imagebyte);
        }
        public void OnGUI()
        {
            if(isrunning)
            {
                if (countdown > 0)
                {
                    GUI.DrawTexture(new Rect(30f, 140f, 160f, 160f), image);
                    GUIStyle style = new GUIStyle();
                    style.fontSize = 60;
                    style.fontStyle = FontStyle.BoldAndItalic;
                    style.normal.textColor = Color.white;

                    GUI.Label(new Rect(180f, 140F, 30F, 30F), Math.Round(countdown).ToString(), style);
                }
            }
            
        }
        public static void StartVision()
        { 
            if(isrunning)
            {
                countdown += 30F;
                SoundLoader.SoundManager.PlaySound(SoundLoader.SoundLoader.nightvision, PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>().transform.position, 2.90F);
            }
            else
            {
                countdown = 30;
                isrunning = true;
                lightobject = new GameObject();
                light = lightobject.AddComponent<Light>();
                light.type = LightType.Directional;
                light.range = 9999999f;
                light.intensity = 3.8f;
                light.color = Color.green;
                DontDestroyOnLoad(lightobject);
                SoundLoader.SoundManager.PlaySound(SoundLoader.SoundLoader.nightvision, PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>().transform.position, 2.90F);
            }
        }
        public void Update()
        {
            if(isrunning)
            {
                if (countdown <= 0)
                {
                    KillLight();
                }
                else
                {
                    countdown -= Time.deltaTime;
                }
            }
            

           
        }

        private void KillLight()
        {
            isrunning = false;
            lightobject.SetActive(false);
            lightobject.transform.position = Camera.main.transform.position;
            Destroy(lightobject);
        }
    }
}
