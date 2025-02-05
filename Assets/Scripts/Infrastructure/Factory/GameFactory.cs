using Core;
using Core.AssetsManagement;
using Core.Models;
using Core.Services.Pool;
using Core.StaticData;
using Core.StaticData.Configs;
using Main;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private HeroMoveConfig _configsHero;
        private UFOConfig _configsEnemy;
        private readonly IInstantiateProvider _instantiate;
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
            return _heroFactory.CreateHero(at, _configsHero);
        }

        

        public GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro)
        {
            return _enemyFactory.CreateEnemy(enemyType, poolContainer, objectPoolAstro, _staticData, _configsEnemy);
        }

        public GameObject CreateHud(ILaserViewModel laserViewModel)
        {
            return _hudFactory.CreateHud(laserViewModel);
        }

        public void LoadConfigs()
        {
            _staticData.LoadStaticData();
            _configsHero = _jsonConfigLoader.LoadConfigsHero();
            _configsEnemy = _jsonConfigLoader.LoadConfigsEnemy();
        }

        public void CleanUp()
        {
        }
    }
}