using Core.Intrerfaces;
using Game;
using Game.InputControllers;
using Infrastructure;
using Infrastructure.Factories;
using UI.MVVM.TestRocketMVVM;
using UnityEngine;
using Zenject;

namespace Main
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LaserManager laserManager;
        [SerializeField] private GameObject asteroidPrefab;
        [SerializeField] private GameObject mediumAsteroid;
        [SerializeField] private GameObject smallAsteroid;
        [SerializeField] private GameObject ufoPrefab;
        [SerializeField] private Transform poolParent;
        public override void InstallBindings()
        {
            Container.Bind<IControlStrategy>().To<KeyboardController>().AsSingle();
            Container.Bind<LaserManager>().FromInstance(laserManager).AsSingle();


            Container.Bind<LaserViewModel>().AsTransient().WithArguments(laserManager);

            // LaserView: Автоматическое связывание View через Zenject
            Container.Bind<LaserView>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<Transform>().FromInstance(poolParent);
            
            Container.Bind<IObjectFactory>().To<ObjectFactory>().AsSingle()
                .WithArguments(asteroidPrefab, ufoPrefab, mediumAsteroid, smallAsteroid);
            Container.Bind<ObjectPoolAstro>().AsSingle().WithArguments(poolParent, Container.Resolve<IObjectFactory>());
            
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}