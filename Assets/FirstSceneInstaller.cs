using System.Collections;
using System.Collections.Generic;
using Core;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces;
using Core.Intrerfaces.Services;
using Core.Intrerfaces.Services.Input;
using Core.Models;
using Core.Services;
using Core.Services.Randomizer;
using Core.StaticData;
using Game;
using Game.Handlers.Health;
using Infrastructure.States;
using Main;
using Main.States;
using UnityEngine;
using Zenject;

public class FirstSceneInstaller : MonoInstaller{
   
        [SerializeField] private MonoBehaviour coroutineRunner; // GameObject с MonoBehaviour для ICoroutineRunner

    public override void InstallBindings() {
            Debug.Log("ALOOOOO");
        // Бинд ICoroutineRunner
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
        
        
        Container.Bind<IInstantiateProvider>().To<InstantiateProvider>().AsSingle();
        Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        Container.Bind<IPlayerDataModel>().To<PlayerDataModel>().AsSingle();
        Container.Bind<IPlayerViewModel>().To<PlayerViewModel>().AsSingle();
        Container.Bind<IScorable>().To<ScoreManager>().AsSingle();
        Container.Bind<IBounceService>().To<BounceService>().AsSingle();
        Container.Bind<ISpawnService>().To<EnemySpawner>().AsSingle();
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        
        Container.Bind<HealthHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IObjectPool>().To<ObjectPoolEnemy>().AsSingle();
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        
        

    }
}
