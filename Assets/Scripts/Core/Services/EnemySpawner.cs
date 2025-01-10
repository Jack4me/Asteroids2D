using Core.Ads_Plugin;
using Core.Analytics;
using Core.Intrerfaces;
using Core.Intrerfaces.Services;
using Core.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services
{
    public class EnemySpawner : ISpawnService
    {
        private readonly IObjectPool _pool;
        private Vector3 spawnPosition;
        private int _delayBetweenAsteroidsSpawn = 5000;
        private int _delayAsteroid = 7000;
        private int _delayBetweenUfoSpawn = 7000;
        private int _delayUFO = 13000;

        public EnemySpawner(IObjectPool pool)
        {
            _pool = pool;
        }

        public async void RunAsyncMethods(SpawnPointsData spawnPointsData)
        {
            await UniTask.WhenAll(
                StartAsyncUFOSpawning(spawnPointsData),
                StartAsyncAsteroidSpawning(spawnPointsData));
        }

        public void SpawnAsteroid(SpawnPointsData spawnPointsData)
        {
            GameObject asteroid = _pool.GetFromPool(EnemyType.Large);
            Vector3 spawnPoint = GetRandomSpawnPoint(spawnPointsData);
            asteroid.transform.position = spawnPoint;
        }

        private async UniTask StartAsyncUFOSpawning(SpawnPointsData spawnPointsData)
        {
            if (GameAnalytics.gameAnalytics == null)
            {
                Debug.LogError("GameAnalytics.gameAnalytics is null.");
            }
            else
            {
                GameAnalytics.gameAnalytics.InterstitialAd();
            }
            
            await UniTask.Delay(_delayBetweenUfoSpawn);
            AdsService.Instance.bannerAds.HideBannerAd();
            while (true)
            {
                SpawnUfo(spawnPointsData);
                await UniTask.Delay(_delayUFO);
            }
        }

        private void SpawnUfo(SpawnPointsData spawnPointsData)
        {
            AdsService.Instance.bannerAds.ShowBannerAd();
            GameObject ufo = _pool.GetFromPool(EnemyType.Ufo);
            Vector3 spawnPoint = GetRandomSpawnPoint(spawnPointsData);
            ufo.transform.position = spawnPoint;
        }

        private async UniTask StartAsyncAsteroidSpawning(SpawnPointsData spawnPointsData)
        {
            await UniTask.Delay(_delayBetweenAsteroidsSpawn);
            while (true)
            {
                SpawnAsteroid(spawnPointsData);
                await UniTask.Delay(_delayAsteroid);
            }
        }

        private Vector3 GetRandomSpawnPoint(SpawnPointsData spawnPointsData)
        {
            if (spawnPointsData != null && spawnPointsData.spawnPositions.Length > 0)
            {
                spawnPosition = spawnPointsData.spawnPositions[Random.Range(0, spawnPointsData.spawnPositions.Length)];
            }
            else
            {
                Debug.LogError("Точки спавна отсутствуют или данные не загружены!");
            }

            return spawnPosition;
        }
    }
}