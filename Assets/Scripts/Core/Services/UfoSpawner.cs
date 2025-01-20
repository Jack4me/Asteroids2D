using Core.Intrerfaces;
using Core.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services
{
    public class UfoSpawner : EnemySpawnController
    {
        private int _delayUFOFirstSpawn = 4000;
        private int _delayBetweenUfoSpawn = 8000;

        public UfoSpawner(IObjectPool pool) : base(pool)
        {
        }

        public async UniTask StartAsyncUFOSpawning(SpawnPointsData spawnPointsData)
        {
            await UniTask.Delay(_delayUFOFirstSpawn);
            while (true)
            {
                SpawnUfo(spawnPointsData);
                await UniTask.Delay(_delayBetweenUfoSpawn);
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