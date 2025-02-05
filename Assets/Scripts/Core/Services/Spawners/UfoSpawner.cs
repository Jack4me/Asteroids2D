using Core.Services.Pool;
using Core.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services.Spawners
{
    public class UfoSpawner : EnemySpawnController
    {
        private const int DelayUFOFirstSpawn = 4000;
        private const int DelayBetweenUfoSpawn = 8000;

        public UfoSpawner(IObjectPool pool) : base(pool)
        {
        }

        public async UniTask StartAsyncUFOSpawning(SpawnPointsData spawnPointsData)
        {
            await UniTask.Delay(DelayUFOFirstSpawn);
            while (true)
            {
                SpawnUfo(spawnPointsData);
                await UniTask.Delay(DelayBetweenUfoSpawn);
            }
        }

        private void SpawnUfo(SpawnPointsData spawnPointsData)
        {
            GameObject ufo = _pool.GetFromPool(EnemyType.Ufo);
            Vector3 spawnPoint = GetRandomSpawnPoint(spawnPointsData);
            ufo.transform.position = spawnPoint;
        }
    }
}