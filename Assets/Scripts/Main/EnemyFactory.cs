using Core;
using Core.AssetsManagement;
using Core.Intrerfaces;
using Core.Services;
using Core.StaticData;
using UnityEngine;

namespace Main
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IInstantiateProvider _instantiate;
        private readonly IBounceService _bounceService;
        private readonly IScorable _scoreManager;

        public EnemyFactory(IInstantiateProvider instantiate, IBounceService bounceService, IScorable scoreManager)
        {
            _instantiate = instantiate;
            _bounceService = bounceService;
            _scoreManager = scoreManager;
        }

        public GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro,
            IStaticDataService staticData, UFOConfig configs)
        {
            var enemyPrefab = staticData.GetEnemyPrefab(enemyType);
            if (enemyPrefab == null)
            {
                Debug.LogError($"No prefab found for enemy type: {enemyType}");
                return null;
            }

            var instance = _instantiate.InstantiateToPool(enemyPrefab, poolContainer);
            Enemy enemyComponent = instance.GetComponent<Enemy>();
            enemyComponent.Initialize(objectPoolAstro, _scoreManager, _bounceService);
            if (enemyComponent.TryGetComponent<IStatsEnemy>(out var stats))
            {
                stats.Damage = configs.damage;
                stats.Health = configs.health;
                stats.Speed = configs.speed;
            }

            return instance;
        }

        public GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro,
            IStaticDataService staticDataService, HeroMoveConfig heroMoveConfig)
        {
            throw new System.NotImplementedException();
        }
    }
}