using Core.Intrerfaces;
using Game.Controllers;
using Game.InputControllers;
using Zenject;

namespace Game {
    public class GameInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IControlStrategy>().To<KeyboardController>().AsSingle();
                // Container.Bind<ShipController>().FromComponentInHierarchy().AsSingle();
        }
    }
}