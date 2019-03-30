

using UnityEngine;
using System;
using System.IO;
namespace Ragnarok.Plugins.ScreenShot
{
    /// <summary>
    /// Screenshot Manager by ice cold
    /// </summary>
    public class ScreenShotMaker : MonoBehaviour
    {//  public WindowsMediaPlayer media;
        public void Start()
        {
        }
		public void Update()
        {
            bool flag = Input.GetKeyDown(KeyCode.F11);
			if(flag)
            {
                try
                {
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Screenshots"))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Screenshots");
                    }                             
                    MakeScreenshot();
                    
                }
                catch(Exception ex)
                {
                    Debug.LogError("Oops, We caught a error during making screenshot. ERROR:  " + ex.Message);
                }
				


            }
        }
        private void MakeScreenshot()
        {

            try
            {
                if (PlayerClient.GetLocalPlayer() != null && PlayerClient.GetLocalPlayer().controllable != null)
                {
                    Vector3 vector = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>().transform.position;
                    Vector3 position = new Vector3(vector.x, vector.y - 10f, vector.z);
                    
                    int count = 0;
                    foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Screenshots\\"))
                    {
                        count++;
                    }

                    Application.CaptureScreenshot(Path.Combine(Directory.GetCurrentDirectory() + "\\Screenshots\\", "screen" + count + ".png"));
                    GameObject go = new GameObject();
                    go = (GameObject)UnityEngine.Object.Instantiate(SoundLoader.SoundLoader.screenshot, vector, new Quaternion(0f, 0, 0f, 0f));
                    UnityEngine.Object.Destroy(go, 1.5F);               
                }
            }
            catch(Exception ex)
            {
                Debug.LogError("Error at making screenshot: " + ex.Message);
            }
           
           
        }
    }
}
