using UnityEngine;

namespace Ragnarok.Plugins.SoundLoader
{
    public class SoundManager : MonoBehaviour
    {
        public static void PlaySound(GameObject sound, Vector3 location, float duration)
        {
            if(sound.name == "ScarryMusic001" || sound.name == "FightClubOOB" || sound.name == "ShipSound001"|| sound.name == "ArmyMusicOOB")
            {
                GameObject go2 = new GameObject();
                go2 = (GameObject)Instantiate(sound, location, new Quaternion(0f, 0f, 0f, 0f));
                return;
            }
            GameObject go = new GameObject();
            go = (GameObject)Instantiate(sound, location, new Quaternion(0f, 0f, 0f, 0f));

            Destroy(go, duration);
        }
    }
}
