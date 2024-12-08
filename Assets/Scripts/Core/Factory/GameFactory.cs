using CodeBase.Infrastructure.AssetsManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.StaticData;
using Core.Intrerfaces;
using Core.Services.Randomizer;
using UnityEngine;
using Zenject;

namespace Core.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiateProvider _instantiate;
        private readonly IRandomService _random;
        private readonly IPlayerDataModel _playerDataModel;
        private readonly IStaticDataService _staticData;

        public GameFactory(IInstantiateProvider instantiate, IStaticDataService staticData, IRandomService random
            , IPlayerDataModel playerDataModel)
        {
            _instantiate = instantiate;
            _staticData = staticData;
            _random = random;
            _playerDataModel = playerDataModel;
        }

        public GameObject HeroGameObject { get; set; }

        public void CreateUnit()
        {
            _instantiate.Instantiate(_staticData.GetUnitConfig().Prefab);
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegister(AssetPath.HERO_PATH, at.transform.position);
            if (_playerDataModel == null)
            {
                Debug.Log("_playerDataModel is NULL");
            }
            HeroGameObject.GetComponent<IPlayerController>().Construct(_playerDataModel);
            _playerDataModel.Position.Value = HeroGameObject.gameObject.transform.position;
            
            Debug.Log(" _playerDataModel.Position.Value" +  _playerDataModel.Position.Value);
            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            var hud = InstantiateRegister(AssetPath.HUD_PATH);
            return hud;
        }

        public void CleanUp()
        {
        }

        private GameObject InstantiateRegister(string path, Vector3 position)
        {
            var gameObject = _instantiate.Instantiate(path, position);
            return gameObject;
        }

        private GameObject InstantiateRegister(string path)
        {
            var gameObject = _instantiate.Instantiate(path);
            return gameObject;
        }
    }
}