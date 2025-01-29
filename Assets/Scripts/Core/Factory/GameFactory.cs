using Core.AssetsManagement;
using Core.Else;
using Core.Game.Entities.Enemies;
using Core.Game.Entities.Hero.Laser;
using Core.Services.Randomizer;
using Core.StaticData;
using Infrastructure;
using Main;
using UnityEngine;

namespace Core.Factory
{
    public class GameFactory : IGameFactory
    {
        private HeroMoveConfig _configsHero;
        private UFOConfig _configsEnemy;
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
            return _heroFactory.CreateHero(at, _configsHero);
        }

        public GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPoolAsteroidGame objectPoolAsteroidGameAstro)
        {
            return _enemyFactory.CreateEnemy(enemyType, poolContainer, objectPoolAsteroidGameAstro, _staticData, _configsEnemy);
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