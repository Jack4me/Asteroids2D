using Core;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces;
using Core.Intrerfaces.Services.Input;
using Core.Models;
using Core.Services;
using Game.Controllers;
using Game.Handlers.Health;
using Game.Hero;
using Infrastructure.UI.View;
using UnityEngine;

namespace Main
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IInputService _inputService;
        private readonly IPlayerDataModel _playerDataModel;
        private readonly IInstantiateProvider _instantiate;
        private IBounceService _bounceService;
        public HeroFactory(IInputService inputService, IPlayerDataModel playerDataModel, IInstantiateProvider instantiate, IBounceService bounceService)
        {
            _inputService = inputService;
            _playerDataModel = playerDataModel;
            _instantiate = instantiate;
            _bounceService = bounceService;
        }

        public GameObject CreateHero(GameObject at, GameConfigs configs)
        {
            var HeroGameObject = _instantiate.Instantiate(AssetPath.HERO_PATH, at.transform.position);
            IPlayerController playerController = HeroGameObject.GetComponent<IPlayerController>();
            playerController.Construct(_playerDataModel);
            _playerDataModel.Position.Value = HeroGameObject.gameObject.transform.position;
            LaserController laserController = HeroGameObject.GetComponent<LaserController>();
            HeroGameObject.GetComponent<HealthHandler>().Construct(_playerDataModel);
            LaserViewModel laserViewModel = new LaserViewModel(laserController);
            HeroGameObject.GetComponent<IPlayerController>().LaserViewModel = laserViewModel;
            HeroGameObject.GetComponent<PlayerCollisionHandler>().Construct(_bounceService);
            HeroGameObject.GetComponent<HeroMove>().Construct(_inputService);
            HeroGameObject.GetComponent<HeroAttack>().Construct(_inputService);
            HeroGameObject.GetComponentInChildren<LaserUIController>().Initialize(laserViewModel);
            
            //remove and move to right place
            if (HeroGameObject.TryGetComponent<IPlayerStats>(out var stats))
            {
                stats.speed = configs.player.speed;
                stats.health = configs.player.health;
                stats.weaponName = configs.player.weaponName;
            }
            
            return HeroGameObject;
            return null;
        }
    }
}