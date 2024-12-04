using CodeBase.Infrastructure.AssetsManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.States {
    internal class BootStrapState : IState {
        private const string INITIAL = "Initial";
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;

        public BootStrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services){
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter(){
            _sceneLoader.Load(INITIAL, EnterLoadLevel);
        }

        public void Exit(){
        }

        private void EnterLoadLevel(){
            _stateMachine.EnterGeneric<LoadProgressState>();
        }

        private void RegisterServices(){
            RegisterStaticData();
            _services.RegisterService<IInstantiateProvider>(new InstantiateProvider());
            _services.RegisterService<IRandomService>(new RandomService());
            _services.RegisterService(RegisterInputServices());
            _services.RegisterService<IGameFactory>(new GameFactory
            (_services.GetService<IInstantiateProvider>(), _services.GetService<IStaticDataService>(),
                _services.GetService<IRandomService>()));
        }

        private void RegisterStaticData(){
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterService(staticData);
        }

        private static IInputService RegisterInputServices(){
            if (Application.isEditor)
                return new StandaloneInputService();
            return new MobileInputService();
        }
    }
}