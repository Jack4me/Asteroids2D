using Core;
using Core.Ads_Plugin;
using Core.Analytics;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces;
using Core.Models;
using Core.StaticData;
using Firebase;
using Firebase.Analytics;
using Game;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.States {
    public class LoadLevelState : ILoadLvlState<string> {
        private const string INITIAL_POINT = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly ISpawnService _spawnService;
       private SpawnPointsData spawnPointsData;
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader){
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            // _gameFactory = gameFactory;
            // _staticDataService = staticDataService;
            // _spawnService = spawnService;
            spawnPointsData = Resources.Load<SpawnPointsData>(AssetPath.SPAWNERS_ENEMY);

        }

        public void Enter(string sceneName){
           // _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit(){
        }

        private void OnLoaded(){
            InitGameWorld();
            
            _gameStateMachine.EnterGeneric<GameLoopState>();
        }

        private void InitGameWorld(){
            InitSpawners();
            _gameFactory.LoadConfigs();
            GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(INITIAL_POINT));
          LaserViewModel laserViewModel =  hero.GetComponent<PlayerController>().LaserViewModel;
           InitHud(laserViewModel);
           
          _gameFactory.CreatePoolParent();
           InitEnemy(spawnPointsData);

        }

        private void InitHud(LaserViewModel laserViewModel){
            var hud = _gameFactory.CreateHud(laserViewModel);
        }

        private void InitEnemy(SpawnPointsData pointsData)
        {
            _spawnService.RunAsyncMethods(pointsData);
        }

        private void InitSpawners(){
            
                        
            // foreach (GameObject spawnerObj in GameObject.FindGameObjectsWithTag(ENEMYSPAWNER)){
            //     var spawner = spawnerObj.GetComponent<EnemySpawner>();
            //     _gameFactory.Register(spawner);
            // }
            var sceneNameKey = SceneManager.GetActiveScene().name;
        }

        public void InitAnalytics()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            GameAnalytics.gameAnalytics.InterstitialAd();
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    Debug.Log("Firebase initialized successfully.");

                    // Логирование тестового события
                    FirebaseAnalytics.LogEvent("test_event");
                    Debug.Log("Test event sent.");
                }
                else
                {
                    Debug.LogError($"Could not resolve Firebase dependencies: {task.Result}");
                }
            });
        }
    }
}