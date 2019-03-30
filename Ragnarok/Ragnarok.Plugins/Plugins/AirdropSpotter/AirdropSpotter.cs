using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Ragnarok.Plugins.Plugins.AirdropSpotter
{
    
    public class AirdropSpotter : MonoBehaviour
    {
        private UnityEngine.Object[] airdrops;
        private Material lineMaterial = new Material("Shader \"Lines/Colored Blended\" {SubShader { Pass {   BindChannels { Bind \"Color\",color }   Blend SrcAlpha OneMinusSrcAlpha   ZWrite Off Cull Off Fog { Mode Off }} } }");
        bool isrunning = false;
        float countdown = 60f;

        public static AirdropSpotter singleton;

        public void Start()
        {
            singleton = this;
            
        }
        public void StartSpotter()
        {
            if(isrunning)
            {
                countdown += 40F;
            }
            else
            {
                isrunning = true;
                this.StartCoroutine(GetObjects());
                countdown = 40f;
            }
           
            
        }
        private IEnumerator GetObjects()
        {
            for(; ; )
            {
                yield return new WaitForSeconds(0.5F);
                if(isrunning)
                {
                    airdrops = UnityEngine.Object.FindObjectsOfType<SupplyCrate>();
                }
            }
        }

        void Update()
        {
            if(isrunning)
            {
                if(countdown <= 0)
                {
                    isrunning = false;

                }
                else
                {
                    countdown -= Time.deltaTime;
                }

               
            }
        }
        void OnGUI()
        {
            
            if(isrunning)
            {
                Vector3 position = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>().transform.position;
                foreach (SupplyCrate crate in airdrops)
                {
                    if (crate.gameObject.name == "SupplyCrate(Clone)")
                    {
                        float distance = Vector3.Distance(position, crate.gameObject.transform.position);
                        if (distance <= 300)
                        {
                            Vector3 vector = Camera.main.WorldToViewportPoint(position) + new Vector3((float)(Screen.width / 2), 0f, 0f);
                            Vector3 vector2 = Camera.main.WorldToScreenPoint(crate.transform.position);
                            bool flag2 = vector2.z < 0f;
                            if (flag2)
                            {
                                continue;
                            }
                            vector.y = (float)Screen.height - vector.y;
                            vector2.y = (float)Screen.height - vector2.y;
                            DrawLine(vector, vector2, Color.yellow);
                        }
                    }
                    
                }
            }
        }

        private void DrawLine(Vector3 pointA, Vector3 pointB, Color color)
        {
            bool flag = lineMaterial == null;
            if (flag)
            {
                lineMaterial = new Material("Shader \"Lines/Colored Blended\" {SubShader { Pass {   BindChannels { Bind \"Color\",color }   Blend SrcAlpha OneMinusSrcAlpha   ZWrite Off Cull Off Fog { Mode Off }} } }");
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
            }
            lineMaterial.SetPass(0);
            GL.Begin(1);
            GL.Color(color);
            GL.Vertex3(pointA.x, pointA.y, 0f);
            GL.Vertex3(pointB.x, pointB.y, 0f);
            GL.End();
        }
    }
}
