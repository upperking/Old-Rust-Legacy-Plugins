using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using System;

namespace Ragnarok.Plugins.Announcer
{
    public class Announcement : MonoBehaviour
    {
        /// <summary>
        /// List that contains all images
        /// </summary>
        private List<Texture2D> images;
        private Rect postition;
        private Texture2D image;

        private bool showimage;

        private string path = RGuard.RGuard.instance.datadir + "Images\\";
        public void Start()
        {
            images = new List<Texture2D>();
            foreach(string file in Directory.GetFiles(path))
            {              
                if(file.Contains("uimage"))
                {
                    Texture2D image = new Texture2D(2, 2, TextureFormat.RGB24, false);
                    image.LoadImage(File.ReadAllBytes(file));
                    images.Add(image);
                    Debug.Log("Image: " + file);
                }
               
            }
            this.StartCoroutine(ShowImage());
        }

        private IEnumerator ShowImage()
        {
            Debug.Log("Started timer");
            yield return new WaitForSeconds(5F);



            Debug.Log("Choosing Image");
            int count = new System.Random().Next(0, images.Count);
            image = images[count];
            int width = Screen.width;
            postition = new Rect((float)(width / 2 - images[count].width / 2), 0f, (float)images[count].width, (float)images[count].height);
            showimage = true;
            this.StartCoroutine(ImageLifeTime());

            yield break;
        }

        private IEnumerator ImageLifeTime()
        {
            yield return new WaitForSeconds(5F);
            showimage = false;
            image = null;
            this.StartCoroutine(ShowImage());
            yield break;
        }

        private void OnGUI()
        {
            if(showimage)
            {
                GUI.DrawTexture(postition, image);
            }
          
        }      
        
    }
}
