using Core;
using Core.Ads_Plugin;
using Core.Analytics;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces.Services;
using Core.Models;
using Core.StaticData;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Infrastructure.States;
using UnityEngine;

namespace Main.States
{
    public class LoadLevelState : ILoadLvlState<string>
    {
        private const string INITIAL_POINT = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly ISpawnService _spawnService;
        private readonly IGameAnalytics _gameAnalytics;
        private readonly IAdsService _adsService;
        private readonly IBannerAds _bannerAds;
        private SpawnPointsData spawnPointsData;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            ISpawnService spawnService, IGameAnalytics gameAnalytics, IAdsService adsService, IBannerAds bannerAds)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _spawnService = spawnService;
            _gameAnalytics = gameAnalytics;
            _adsService = adsService;
            _bannerAds = bannerAds;
            spawnPointsData = Resources.Load<SpawnPointsData>(AssetPath.SPAWNERS_ENEMY);
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.EnterGeneric<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.LoadConfigs();
            GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(INITIAL_POINT));
            ILaserViewModel laserViewModel = hero.GetComponent<PlayerController>().LaserViewModel;
            InitHud(laserViewModel);
            _gameFactory.CreatePoolParent();
            InitEnemy(spawnPointsData);
            RunAnalytics();
        }

        private async void RunAnalytics()
        {
            
            _adsService.InitializeAds();
            await WaitForSecondsAsync(3f);
        }

        public async UniTask WaitForSecondsAsync(float delayTime)
        {
            Debug.Log("Delay started.");
            await UniTask.Delay((int)(delayTime * 1000));
            InitAnalytics();
            _bannerAds.ShowBannerAd();
            Debug.Log("Delay ended.");
        }

        private void InitHud(ILaserViewModel laserViewModel)
        {
            var hud = _gameFactory.CreateHud(laserViewModel);
        }

        private void InitEnemy(SpawnPointsData pointsData)
        {
            _spawnService.RunAsyncMethods(pointsData);
        }

        public void InitAnalytics()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
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