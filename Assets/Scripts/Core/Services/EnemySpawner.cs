using System.Threading.Tasks;
using Core.Intrerfaces;
using UnityEngine;

namespace Core.Services
{
    public class EnemySpawner : ISpawnService
    {
        private readonly IObjectPool _pool;

        public EnemySpawner(IObjectPool pool)
        {
            _pool = pool;
        }


        private async void RunAsyncMethods()
        {
            await StartAsteroidSpawning();
        }

        private async Task StartAsteroidSpawning()
        {
            await Task.Delay(1000);

            while (true)
            {
                SpawnAsteroid();

                await Task.Delay(5000);
            }
        }

        public void SpawnAsteroid()
        {
           GameObject asteroid = _pool.GetFromPool(EnemyType.Large);
            // Transform spawnPoint = GetRandomSpawnPoint();
            // asteroid.transform.position = spawnPoint.position;

           
        }

        // private Transform GetRandomSpawnPoint()
        // {
        //     int randomIndex = Random.Range(0, spawnPoints.Length);
        //     return spawnPoints[randomIndex];
        // }
    }
}