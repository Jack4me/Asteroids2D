using Core.Intrerfaces;
using Core.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services
{
    public class AsteroidSpawner : EnemySpawnController
    {
        private int _delayBetweenAsteroidsSpawn = 3000;
        private int _delayAsteroid = 7000;

        public AsteroidSpawner(IObjectPool pool) : base(pool)
        {
        }

        public async UniTask StartAsyncAsteroidSpawning(SpawnPointsData spawnPointsData)
        {
            await UniTask.Delay(_delayBetweenAsteroidsSpawn);
            while (true)
            {
                SpawnAsteroid(spawnPointsData);
                await UniTask.Delay(_delayAsteroid);
            }
        }

        public void SpawnAsteroid(SpawnPointsData spawnPointsData)
        {
            GameObject asteroid = _pool.GetFromPool(EnemyType.Large);
            Vector3 spawnPoint = GetRandomSpawnPoint(spawnPointsData);
            asteroid.transform.position = spawnPoint;
        }
    }
}
