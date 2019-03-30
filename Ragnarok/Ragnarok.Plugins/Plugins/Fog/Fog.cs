using UnityEngine;

namespace Ragnarok.Plugins.Fog
{
    public class Fog : MonoBehaviour
    {
        public void Start()
        {
        }
        public void Update()
        {
            //RenderSettings.fog = true;
            //RenderSettings.fogColor = Color.white;
            //RenderSettings.fogDensity = 0.046F;
          //  RenderSettings.fogMode = FogMode.ExponentialSquared;
        }
        public static void EnableFog()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.white;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
        }
        public static void DisableFog()
        {
            RenderSettings.fog = false;
            
        }
        public static void ChangeFogDensity(float amount)
        {
            RenderSettings.fogDensity = amount;
        }
    }
}
