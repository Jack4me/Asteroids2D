using Core.Intrerfaces;
using Game.InputControllers;
using UnityEngine;
using Zenject;

namespace Game {
    public class GameInstaller : MonoInstaller {
        [SerializeField] private LaserManager laserManager;
        public override void InstallBindings() {
            Container.Bind<IControlStrategy>().To<KeyboardController>().AsSingle();
            Container.Bind<LaserManager>().FromInstance(laserManager).AsSingle();
                // Container.Bind<ShipController>().FromComponentInHierarchy().AsSingle();
        }
    }
}