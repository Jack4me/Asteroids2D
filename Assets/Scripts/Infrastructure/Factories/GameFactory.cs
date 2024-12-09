using System.IO;
using CodeBase.Infrastructure.AssetsManagement;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces;
using Core.Services.Randomizer;
using Core.StaticData;
using Game.Config;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiateProvider _instantiate;
        private readonly IRandomService _random;
        private readonly IPlayerDataModel _playerDataModel;
        private readonly IStaticDataService _staticData;
        private readonly IPlayerViewModel _viewModelPlayer;
        private readonly IConfigLoader _configLoader;
        private GameConfigs _configs;

        public GameFactory(IInstantiateProvider instantiate, IStaticDataService staticData, IRandomService random
            , IPlayerDataModel playerDataModel, IPlayerViewModel viewModelPlayer, IConfigLoader configLoader)
        {
            _instantiate = instantiate;
            _staticData = staticData;
            _random = random;
            _playerDataModel = playerDataModel;
            _viewModelPlayer = viewModelPlayer;
            _configLoader = configLoader;
        }

        public GameObject HeroGameObject { get; set; }

        public void CreateUnit()
        {
            _instantiate.Instantiate(_staticData.GetUnitConfig().Prefab);
        }

        public void LoadConfigs()
        {
            string filePath = Path.Combine(Application.dataPath, "Configs.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                  _configs = JsonUtility.FromJson<GameConfigs>(json);
            }
            else
            {
                Debug.LogError($"JSON файл не найден по пути: {filePath}");
            }
        }
        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegister(AssetPath.HERO_PATH, at.transform.position);
          
            HeroGameObject.GetComponent<IPlayerController>().Construct(_playerDataModel, _configLoader);
            _playerDataModel.Position.Value = HeroGameObject.gameObject.transform.position;
            
            // Используем интерфейс для настройки параметров
            if (HeroGameObject.TryGetComponent<IPlayerStats>(out var stats))
            {
                stats.health = _configs.player.health;
                stats.speed = _configs.player.speed;
                stats.weaponName = _configs.player.weaponName;
            }
            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            var hud = InstantiateRegister(AssetPath.HUD_PATH);
            //hud.GetComponent<IPlayerUIView>().Construct(_viewModelPlayer);
            return hud;
        }

        public void CleanUp()
        {
        }

        private GameObject InstantiateRegister(string path, Vector3 position)
        {
            var gameObject = _instantiate.Instantiate(path, position);
            return gameObject;
        }

        private GameObject InstantiateRegister(string path)
        {
            var gameObject = _instantiate.Instantiate(path);
            return gameObject;
        }
    }
}