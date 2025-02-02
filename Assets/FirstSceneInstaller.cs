using Asteroid.UI;
using Core;
using Core.Ads_Plugin;
using Core.Analytics;
using Core.AssetsManagement;
using Core.Factory;
using Core.Game;
using Core.Game.Entities.Hero;
using Core.Game.Handlers.Health;
using Core.Intrerfaces;
using Core.Intrerfaces.Services.Input;
using Core.Models;
using Core.Services;
using Core.Services.Intrerfaces;
using Core.Services.Pool;
using Core.StaticData;
using Infrastructure.Bootstrap;
using Infrastructure.Factory;
using Infrastructure.Init;
using Infrastructure.States;
using Main;
using Main.States;
using UnityEngine;
using Zenject;

public class FirstSceneInstaller : MonoInstaller
{
    [SerializeField] private MonoBehaviour coroutineRunner;

    public override void InstallBindings()
    {
        if (Application.isEditor)
        {
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
        }
        else
        {
            Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
        }
      
        Container.Bind<ICoroutineRunner>()
            .To<CoroutineRunnerWrapper>()
            .AsSingle()
            .WithArguments(coroutineRunner);
        Container.Bind<SceneLoader>()
            .AsSingle();
        Container.Bind<BootStrapState>().AsSingle();
        Container.Bind<LoadLevelState>().AsSingle();
        Container.Bind<LoadProgressState>().AsSingle();
        Container.Bind<GameLoopState>().AsSingle();
        Container.Bind<GameStateMachine>()
            .AsSingle();
        Container.Bind<GameInit>()
            .AsSingle();
        Container.Bind<IGameAnalytics>().To<GameAnalytics>().AsSingle();
        Container.Bind<IBannerAds>().To<BannerAds>().AsSingle();
        Container.Bind<IInterstitialAds>().To<InterstitialAds>().AsSingle();
        Container.Bind<IRewardedAds>().To<RewardedAds>().AsSingle();
        Container.Bind<IAdsService>().To<AdsService>().AsSingle();
        Container.Bind<IInstantiateProvider>().To<InstantiateProvider>().AsSingle();
        Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        Container.Bind<IPlayerDataModel>().To<PlayerDataModel>().AsSingle();
        Container.Bind<IPlayerViewModel>().To<PlayerViewModel>().AsSingle();
        Container.Bind<IScorable>().To<ScoreService>().AsSingle();
        Container.Bind<IBounceService>().To<BounceService>().AsSingle();
        Container.Bind<ISpawnService>().To<EnemySpawnController>().AsSingle();
        Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        Container.Bind<ILaserController>().To<LaserController>().AsSingle();
        Container.Bind<ILaserViewModel>().To<LaserViewModel>().AsSingle();
        Container.Bind<IHudFactory>().To<HudFactory>().AsSingle();
        Container.Bind<IJsonConfigLoader>().To<JsonConfigLoader>().AsSingle();
        Container.Bind<HealthHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IObjectPool>().To<ObjectPoolEnemy>().AsSingle();
        
      
        
        
        
    }
}