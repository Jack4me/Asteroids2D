using Core;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces;
using Core.Intrerfaces.Services.Input;
using Core.Models;
using Core.Services;
using Core.Services.Randomizer;
using Core.StaticData;
using Game;
using Game.Controllers;
using Game.Handlers.Health;
using Game.Hero;
using Infrastructure.UI_MVVM.View;
using UnityEngine;

namespace Main
{
    public class GameFactory : IGameFactory
    {
        public GameObject HeroGameObject { get; set; }
        private const string ENEMYSPAWNER = "EnemySpawner";

        private readonly IInstantiateProvider _instantiate;
        private readonly IRandomService _random;
        private readonly IPlayerDataModel _playerDataModel;
        private readonly IStaticDataService _staticData;
        private readonly IPlayerViewModel _viewModelPlayer;
        private readonly IScorable _scoreManager;
        private readonly IBounceService _bounceService;
        private readonly IInputService _inputService;
        private readonly IConfigLoader _configLoader;
        private GameConfigs _configs;


        public GameFactory(
            IInstantiateProvider instantiate, IStaticDataService staticData, 
            IPlayerDataModel playerDataModel, IPlayerViewModel viewModelPlayer,
            IScorable scoreManager, IBounceService bounceService, IInputService inputService)
        {
            _instantiate = instantiate;
            _staticData = staticData;
            _playerDataModel = playerDataModel;
            _viewModelPlayer = viewModelPlayer;
            _scoreManager = scoreManager;
            _bounceService = bounceService;
            _inputService = inputService;
        }
        public Transform CreatePoolParent()
        {
            GameObject pool =  _instantiate.Instantiate(AssetPath.POOL_PATH);
            return pool.transform;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegister(AssetPath.HERO_PATH, at.transform.position);

            IPlayerController playerController = HeroGameObject.GetComponent<IPlayerController>();
            playerController.Construct(_playerDataModel);
            _playerDataModel.Position.Value = HeroGameObject.gameObject.transform.position;
            LaserManager laserManager = HeroGameObject.GetComponent<LaserManager>();
           HeroGameObject.GetComponent<HealthHandler>().Construct(_playerDataModel);
            LaserViewModel laserViewModel = new LaserViewModel(laserManager);
            HeroGameObject.GetComponent<IPlayerController>().LaserViewModel = laserViewModel ; 
            HeroGameObject.GetComponent<PlayerCollisionHandler>().Construct(_bounceService);
            HeroGameObject.GetComponent<HeroMove>().Construct(_inputService);
            HeroGameObject.GetComponent<HeroAttack>().Construct(_inputService);
                       
     
            //remove and move to right place
            if (HeroGameObject.TryGetComponent<IPlayerStats>(out var stats))
            {
                stats.speed = _configs.player.speed;
                stats.health = _configs.player.health;
                stats.weaponName = _configs.player.weaponName;
            }

            return HeroGameObject;
        }

        public GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro)
        {
            var enemyPrefab = _staticData.GetEnemyPrefab(enemyType);
            if (enemyPrefab == null)
            {
                Debug.LogError($"No prefab found for enemy type: {enemyType}");
                return null;
            }
        
            var instance = _instantiate.InstantiateToPool(enemyPrefab, poolContainer);
       
            Enemy enemyComponent = instance.GetComponent<Enemy>();
            enemyComponent.Initialize(objectPoolAstro, _scoreManager, _bounceService);
            if (enemyComponent.TryGetComponent<IStatsEnemy>(out var stats))
            {
                stats.Damage = _configs.enemy.damage;
                stats.Health = _configs.enemy.health;
                stats.Speed = _configs.enemy.speed;
             
            }
            return instance;

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

        public GameObject CreateHud(LaserViewModel laserViewModel)
        {
            var hud = InstantiateRegister(AssetPath.HUD_PATH);
            hud.GetComponent<PlayerUIView>().Construct(_viewModelPlayer);
            hud.GetComponent<LaserView>().Construct(laserViewModel);
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