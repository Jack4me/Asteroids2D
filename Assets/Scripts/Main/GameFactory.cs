using Core;
using Core.AssetsManagement;
using Core.Factory;
using Core.Intrerfaces;
using Core.Models;
using Core.Services.Randomizer;
using Core.StaticData;
using UnityEngine;

namespace Main
{
    public class GameFactory : IGameFactory
    {
        private GameConfigs _configs;
        private readonly IInstantiateProvider _instantiate;
        private readonly IRandomService _random;
        private readonly IStaticDataService _staticData;
        private readonly IHeroFactory _heroFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IHudFactory _hudFactory;
        private readonly IJsonConfigLoader _jsonConfigLoader;

        public GameFactory(IInstantiateProvider instantiate, IStaticDataService staticData, IHeroFactory heroFactory,
            IEnemyFactory enemyFactory, IHudFactory hudFactory, IJsonConfigLoader jsonConfigLoader)
        {
            _instantiate = instantiate;
            _staticData = staticData;
            _heroFactory = heroFactory;
            _enemyFactory = enemyFactory;
            _hudFactory = hudFactory;
            _jsonConfigLoader = jsonConfigLoader;
        }

        public Transform CreatePoolParent()
        {
            GameObject pool = _instantiate.Instantiate(AssetPath.POOL_PATH);
            return pool.transform;
        }

        public GameObject CreateHero(GameObject at)
        {
            return _heroFactory.CreateHero(at, _configs);
        }

        public GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro)
        {
            return _enemyFactory.CreateEnemy(enemyType, poolContainer, objectPoolAstro, _staticData, _configs);
        }

        public GameObject CreateHud(LaserViewModel laserViewModel)
        {
            return _hudFactory.CreateHud(laserViewModel);
        }

        public void LoadConfigs()
        {
            _configs = _jsonConfigLoader.LoadConfigs();
        }

        public void CleanUp()
        {
        }
    }
}