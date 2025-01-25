using Core;
using Core.StaticData;
using UnityEngine;

namespace Main
{
    public class JsonConfigLoader : IJsonConfigLoader
    {
        private HeroMoveConfig _configs;

       
        public HeroMoveConfig LoadConfigsHero()
        {
            //_staticData.LoadStaticData();
            TextAsset jsonFile = Resources.Load<TextAsset>("HeroMoveConfig");
            if (jsonFile != null)
            {
             return  JsonUtility.FromJson<HeroMoveConfig>(jsonFile.text);
            }
            else
            {
                Debug.LogError("JSON файл не найден в папке Resources");
            }
            return null;
        }
        public UFOConfig LoadConfigsEnemy()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("UFOConfig");
            if (jsonFile != null)
            {
                return  JsonUtility.FromJson<UFOConfig>(jsonFile.text);
            }
            else
            {
                Debug.LogError("JSON файл не найден в папке Resources");
            }
            return null;
        }
        public LaserControllerConfig LoadConfigLaser()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("LaserControllerConfig");
            if (jsonFile != null)
            {
                return  JsonUtility.FromJson<LaserControllerConfig>(jsonFile.text);
            }
            else
            {
                Debug.LogError("JSON файл не найден в папке Resources");
            }
            return null;
        }
    }
}
