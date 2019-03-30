using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Ragnarok.Plugins.SoundLoader
{
    public class SoundLoader : MonoBehaviour
    {
        public static GameObject screenshot;
        public static GameObject treasure;
        public static GameObject build;
        public static GameObject scream;
        public static GameObject scarymusic;
        public static GameObject fightclubmusic;
        public static GameObject shipsound1;
        public static GameObject armymusic;
        public static GameObject nightvision;

        public AssetBundle bundle;

        public string path = RGuard.RGuard.instance.datadir + "Sounds\\Sounds.unity3d";

        public IEnumerator Start()
        {
            WWW www = WWW.LoadFromCacheOrDownload("file://" + path, 1);
            yield return www;
            bundle = www.assetBundle;
            www.Dispose();

            this.StartCoroutine(LoadSounds());
            www = null;
            yield break;
        }

        private IEnumerator LoadSounds()
        {
            screenshot = (bundle.Load("ScreenshotOB", typeof(GameObject)) as GameObject);
            treasure = (bundle.Load("TreasureOB", typeof(GameObject)) as GameObject);
            build = (bundle.Load("BuildSoundOB", typeof(GameObject)) as GameObject);
            scream = (bundle.Load("ScreamOB", typeof(GameObject)) as GameObject);
            scarymusic = (bundle.Load("ScarryMusic001", typeof(GameObject)) as GameObject);
            fightclubmusic = (bundle.Load("FightClubOOB", typeof(GameObject)) as GameObject);
            shipsound1 = (bundle.Load("ShipSound001", typeof(GameObject)) as GameObject);
            armymusic = (bundle.Load("ArmyMusicOOB", typeof(GameObject)) as GameObject);
            nightvision = (bundle.Load("NightVisionOB", typeof(GameObject)) as GameObject);
            bundle.Unload(false);
            yield return null;
            yield break;
        }
    }
}
