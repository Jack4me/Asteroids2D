using Core;
using Core.AssetsManagement;
using Core.Services;
using Core.Services.Pool;
using Core.StaticData;
using Core.StaticData.Configs;
using UnityEngine;

namespace Infrastructure.Factory
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
            Enemies enemiesComponent = instance.GetComponent<Enemies>();
            enemiesComponent.Initialize(objectPoolAstro, _scoreManager, _bounceService);
            if (enemiesComponent.TryGetComponent<IStatsEnemy>(out var stats))
            {
                stats.Damage = configs.damage;
                stats.Health = configs.health;
                stats.Speed = configs.speed;
            }

            return instance;
        }
    }
}