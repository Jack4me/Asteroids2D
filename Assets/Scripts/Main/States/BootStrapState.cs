﻿using CodeBase.Infrastructure.Services.Randomizer;
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
using Infrastructure;
using Infrastructure.Ref.Services;
using Infrastructure.States;
using UnityEngine;

namespace Main.States
{
    internal class BootStrapState : IState
    {
        private const string INITIAL_LEVEL = "Initial";
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        private readonly Transform _transform;

        public BootStrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services
           )
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
           

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(INITIAL_LEVEL, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.EnterGeneric<LoadProgressState>();
        }
       
        private void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterService<IInstantiateProvider>(new InstantiateProvider());
            _services.RegisterService<IRandomService>(new RandomService());
            _services.RegisterService(RegisterInputServices());
            _services.RegisterService<IPlayerDataModel>(new PlayerDataModel());
            _services.RegisterService<IPlayerViewModel>(new PlayerViewModel());
            _services.RegisterService<IScorable>(new ScoreManager(_services.GetService<IPlayerDataModel>()));
            _services.RegisterService<IBounceService>(new BounceService());

            _services.RegisterService<IGameFactory>(new GameFactory
            (_services.GetService<IInstantiateProvider>(), _services.GetService<IStaticDataService>(),
                _services.GetService<IRandomService>(), _services.GetService<IPlayerDataModel>(),
                _services.GetService<IPlayerViewModel>(), _services.GetService<IScorable>(), _services.GetService<IBounceService>()));
            _services.RegisterService<IObjectPool>(new ObjectPoolEnemy());
            _services.RegisterService<ISpawnService>(new EnemySpawner(_services.GetService<IObjectPool>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            //  staticData.Load();
            _services.RegisterService(staticData);
        }

        private static IInputService RegisterInputServices()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            return new MobileInputService();
        }
    }
}