using Core.Intrerfaces;
using Game.InputControllers;
using UI.Test.TestRocketMVVM;
using UnityEngine;
using Zenject;

namespace Game {
    public class GameInstaller : MonoInstaller {
        [SerializeField] private LaserManager laserManager;
        [SerializeField] private  LaserModel laserModel ;
        public override void InstallBindings() {
            Container.Bind<IControlStrategy>().To<KeyboardController>().AsSingle();
            Container.Bind<LaserManager>().FromInstance(laserManager).AsSingle();
            
            
            Container.Bind<LaserModel>().AsSingle().WithArguments(10); // 10 - стартовое количество ракет
        
            // Указываем Zenject, как создавать ViewModel
            Container.Bind<LaserViewModel>().AsTransient().WithArguments(Container.Resolve<LaserModel>());
        
            // Биндим View
            Container.Bind<LaserView>().FromComponentInHierarchy().AsSingle();
        }
    }
}