using Core;
using Core.StaticData;
using UnityEngine;

namespace Main
{
    public class JsonConfigLoader : IJsonConfigLoader
    {
        private GameConfigs _configs;
        private readonly IStaticDataService _staticData;

        JsonConfigLoader(IStaticDataService staticData)
        {
            _staticData = staticData;
        }
        public GameConfigs LoadConfigs()
        {
            _staticData.LoadStaticData();
            TextAsset jsonFile = Resources.Load<TextAsset>("Configs");
            if (jsonFile != null)
            {
             return  JsonUtility.FromJson<GameConfigs>(jsonFile.text);
            }
            else
            {
                Debug.LogError("JSON файл не найден в папке Resources");
            }
            return null;
        }
    }
}
