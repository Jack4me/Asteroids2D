using Asteroid.UI;
using Core.AssetsManagement;
using Core.Game.Entities;
using Core.Game.Entities.Hero;
using Core.Game.Entities.Hero.Invincibility;
using Core.Game.Handlers.Health;
using Core.Intrerfaces;
using Core.Intrerfaces.Services.Input;
using Core.Models;
using Core.Services;
using Core.Services.Intrerfaces;
using Core.StaticData;
using Main;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IInputService _inputService;
        private readonly IPlayerDataModel _playerDataModel;
        private readonly IInstantiateProvider _instantiate;
        private readonly IBounceService _bounceService;
        private readonly ILaserController _laserController;
        private readonly ILaserViewModel _laserViewModel;
        private readonly DiContainer _container;

        public HeroFactory(IInputService inputService, IPlayerDataModel playerDataModel,
            IInstantiateProvider instantiate, IBounceService bounceService, ILaserController laserController,
            ILaserViewModel laserViewModel, DiContainer container)
        {
            _inputService = inputService;
            _playerDataModel = playerDataModel;
            _instantiate = instantiate;
            _bounceService = bounceService;
            _laserController = laserController;
            _laserViewModel = laserViewModel;
            _container = container;
        }

        public GameObject CreateHero(GameObject at, HeroMoveConfig configs)
        {
            var HeroGameObject = _instantiate.Instantiate(AssetPath.HERO_PATH, at.transform.position);
            IPlayerController playerController = HeroGameObject.GetComponent<IPlayerController>();
            playerController.Construct(_playerDataModel);
            _playerDataModel.Position.Value = HeroGameObject.gameObject.transform.position;
            HeroGameObject.GetComponent<HealthHandler>().Construct(_playerDataModel);
            HeroGameObject.GetComponent<IPlayerController>().LaserViewModel = _laserViewModel;
            HeroGameObject.GetComponent<HeroCollisionHandler>().Construct(_bounceService);
            HeroGameObject.GetComponent<HeroInput>().Construct(_inputService);
            HeroGameObject.GetComponent<HeroAttack>().Construct(_inputService, _laserController, _container);
            HeroGameObject.GetComponentInChildren<LaserUIController>().Initialize(_laserViewModel);
            if (HeroGameObject.TryGetComponent<IPlayerStats>(out var stats))
            {
                stats.speed = configs.speed;
                stats.health = configs.health;
                stats.weaponName = configs.weaponName;
            }

            return HeroGameObject;
        }
    }
}