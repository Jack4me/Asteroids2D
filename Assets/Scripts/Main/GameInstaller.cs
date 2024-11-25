using Core.Intrerfaces;
using Game;
using Game.InputControllers;
using UI.MVVM.TestRocketMVVM;
using UnityEngine;
using Zenject;

namespace Main
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LaserManager laserManager;

        public override void InstallBindings()
        {
            Container.Bind<IControlStrategy>().To<KeyboardController>().AsSingle();
            Container.Bind<LaserManager>().FromInstance(laserManager).AsSingle();


            Container.Bind<LaserViewModel>().AsTransient().WithArguments(laserManager);

            // LaserView: Автоматическое связывание View через Zenject
            Container.Bind<LaserView>().FromComponentInHierarchy().AsSingle();
        }
    }
}