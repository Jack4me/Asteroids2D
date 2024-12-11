using Core;
using Core.Intrerfaces;
using Core.Models;
using Game;
using Game.Controllers;
using Game.Controllers.InputControllers;
using Game.InputControllers;
using Game.Models;
using Infrastructure;
using Infrastructure.Factories;
using UI.MVVM.View;
using UnityEngine;
using Zenject;

namespace Main
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LaserManager laserManager;
        [SerializeField] private Transform poolParent;
        public override void InstallBindings()
        {
           
          //  Container.Bind<IControlStrategy>().To<KeyboardController>().AsSingle();
            Container.Bind<LaserManager>().FromInstance(laserManager).AsSingle();

            // Привязываем лазеры
            Container.Bind<LaserViewModel>().AsTransient().WithArguments(laserManager);
            Container.Bind<LaserView>().FromComponentInHierarchy().AsSingle();

            // Привязываем PoolParent
            Container.Bind<Transform>().FromInstance(poolParent);

            
            // Пул объектов
            Container.Bind<IObjectPool>().To<ObjectPoolEnemy>().AsSingle().WithArguments(poolParent);

            // Создание GameManager через Zenject
            Container.Bind<GameManager>().FromNewComponentOnNewGameObject().AsSingle();



            
            // Container.Bind<IPlayerDataModel>().To<PlayerDataModel>().AsSingle();
            // Container.Bind<PlayerViewModel>().AsSingle().WithArguments(Container.Resolve<IPlayerDataModel>());
            
           // Container.Bind<IScorable>().To<ScoreManager>().AsSingle();
            Container.Bind<PlayerViewModel>().AsSingle();

            

            
        }
    }
}