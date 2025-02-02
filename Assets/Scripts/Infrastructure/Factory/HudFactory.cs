using Asteroid.UI;
using Core.AssetsManagement;
using Core.Models;
using Core.Services;
using Main;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class HudFactory : IHudFactory
    {
        private IInstantiateProvider _instantiate;
        private readonly IPlayerViewModel _viewModelPlayer;

        public HudFactory(IInstantiateProvider instantiate, IPlayerViewModel viewModelPlayer)
        {
            _instantiate = instantiate;
            _viewModelPlayer = viewModelPlayer;
        }

        public GameObject CreateHud(ILaserViewModel laserViewModel)
        {
            var hud = _instantiate.Instantiate(AssetPath.HUD_PATH);
            hud.GetComponent<PlayerUIView>().Construct(_viewModelPlayer);
            hud.GetComponent<LaserView>().Construct(laserViewModel);
            return hud;
        }
    }
}