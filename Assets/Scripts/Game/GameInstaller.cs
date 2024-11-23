using Core.Intrerfaces;
using Game.InputControllers;
using Game.MVVM.TestRocketMVVM;
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
            
            
        
            
            Container.Bind<LaserViewModel>().AsTransient().WithArguments(laserManager);

            // LaserView: Автоматическое связывание View через Zenject
            Container.Bind<LaserView>().FromComponentInHierarchy().AsSingle();
        }
    }
}