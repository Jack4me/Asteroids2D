using System.Threading.Tasks;
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
        Vector3 spawnPosition;

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
            await UniTask.Delay(3000);
            AdsManager.Instance.bannerAds.HideBannerAd();
            while (true)
            {
                SpawnUfo(spawnPointsData);

                await UniTask.Delay(5000);
            }
           
        }

        private void SpawnUfo(SpawnPointsData spawnPointsData)
        {
            AdsManager.Instance.bannerAds.ShowBannerAd();

            GameObject ufo = _pool.GetFromPool(EnemyType.Ufo);
            Vector3 spawnPoint = GetRandomSpawnPoint(spawnPointsData);

            ufo.transform.position = spawnPoint;
        }

        private async UniTask StartAsyncAsteroidSpawning(SpawnPointsData spawnPointsData)
        {
            await UniTask.Delay(1000);

            while (true)
            {
                SpawnAsteroid(spawnPointsData);

                await UniTask.Delay(5000);
            }
        }

        public void SpawnAsteroid(SpawnPointsData spawnPointsData)
        {
            GameObject asteroid = _pool.GetFromPool(EnemyType.Large);
            Vector3 spawnPoint = GetRandomSpawnPoint(spawnPointsData);
            asteroid.transform.position = spawnPoint;
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