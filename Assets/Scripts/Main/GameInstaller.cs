using Core;
using Core.Intrerfaces;
using Game;
using Game.Controllers;
using Game.InputControllers;
using Game.Models;
using Infrastructure;
using Infrastructure.Factories;
using UI.MVVM.TestRocketMVVM;
using UI.MVVM.View;
using UnityEngine;
using Zenject;

namespace Main
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LaserManager laserManager;
        [SerializeField] private PlayerController player; 
        [SerializeField] private GameObject asteroidPrefab;
        [SerializeField] private GameObject mediumAsteroid;
        [SerializeField] private GameObject smallAsteroid;
        [SerializeField] private GameObject ufoPrefab;
        [SerializeField] private Transform poolParent;
        public override void InstallBindings()
        {
           
            Container.Bind<IControlStrategy>().To<KeyboardController>().AsSingle();
            Container.Bind<LaserManager>().FromInstance(laserManager).AsSingle();

            // Привязываем лазеры
            Container.Bind<LaserViewModel>().AsTransient().WithArguments(laserManager);
            Container.Bind<LaserView>().FromComponentInHierarchy().AsSingle();

            // Привязываем PoolParent
            Container.Bind<Transform>().FromInstance(poolParent);

            // Фабрика объектов
            Container.Bind<IObjectFactory>().To<ObjectFactory>().AsSingle()
                .WithArguments(asteroidPrefab, ufoPrefab, mediumAsteroid, smallAsteroid);

            // Пул объектов
            Container.Bind<ObjectPoolAstro>().AsSingle().WithArguments(poolParent, Container.Resolve<IObjectFactory>());

            // Создание GameManager через Zenject
            Container.Bind<GameManager>().FromNewComponentOnNewGameObject().AsSingle();

            
            
            Container.Bind<PlayerDataModel>().AsSingle();

            // Создание ViewModel с привязкой к модели
            Container.Bind<PlayerViewModel>().AsSingle().WithArguments(Container.Resolve<PlayerDataModel>());
            
            Container.Bind<ScoreManager>()
                .FromComponentInHierarchy() // Находит ScoreManager в сцене
                .AsSingle();

            
        }
    }
}