using System.IO;
using Core;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces;
using Core.Services.Randomizer;
using Core.StaticData;
using ModestTree;
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
        private readonly IScorable _scoreManager;
        private readonly IConfigLoader _configLoader;
        private GameConfigs _configs;

        public GameFactory(IInstantiateProvider instantiate, IStaticDataService staticData, IRandomService random
            , IPlayerDataModel playerDataModel, IPlayerViewModel viewModelPlayer, IScorable scoreManager)
        {
            _instantiate = instantiate;
            _staticData = staticData;
            _random = random;
            _playerDataModel = playerDataModel;
            _viewModelPlayer = viewModelPlayer;
            _scoreManager = scoreManager;
        }

        public GameObject HeroGameObject { get; set; }

       


        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegister(AssetPath.HERO_PATH, at.transform.position);

            HeroGameObject.GetComponent<IPlayerController>().Construct(_playerDataModel);
            _playerDataModel.Position.Value = HeroGameObject.gameObject.transform.position;

            if (HeroGameObject.TryGetComponent<IPlayerStats>(out var stats))
            {
                stats.speed = _configs.player.speed;
                stats.health = _configs.player.health;
                stats.weaponName = _configs.player.weaponName;
            }

            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            var hud = InstantiateRegister(AssetPath.HUD_PATH);

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

        public void LoadConfigs()
        {
            _staticData.LoadStaticData();

            TextAsset jsonFile = Resources.Load<TextAsset>("Configs");
            if (jsonFile != null)
            {
                _configs = JsonUtility.FromJson<GameConfigs>(jsonFile.text);
            }
            else
            {
                Debug.LogError("JSON файл не найден в папке Resources");
            }

        }

       

        public GameObject CreateEnemy(EnemyType enemyType, Transform poolParent, IObjectPool objectPoolAstro)
        {
            var enemyPrefab = _staticData.GetEnemyPrefab(enemyType);
            if (enemyPrefab == null)
            {
                Debug.LogError($"No prefab found for enemy type: {enemyType}");
                return null;
            }

            var instance = _instantiate.InstantiateToPool(enemyPrefab, poolParent);
            // var instance = _instantiate.Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity, parent);
            Enemy enemyComponent = instance.GetComponent<Enemy>();
             enemyComponent.Initialize(objectPoolAstro, _scoreManager);
             if (enemyComponent.TryGetComponent<IStatsEnemy>(out var stats))
             {
                 stats.Damage = _configs.enemy.damage;
                 stats.Health = _configs.enemy.health;
                 stats.Speed = _configs.enemy.speed;
             
             }
            return instance;

        }
    }
}