using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Ads_Plugin;
using Core.Analytics;
using Core.Intrerfaces;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Infrastructure;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameService : MonoBehaviour
    {
        [SerializeField] private GameObject asteroidPrefab;

        [SerializeField] private GameObject ufoPrefab;

        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private int maxAsteroids = 10;
        [SerializeField] private int maxUFOs = 3;

        private IObjectPool poolAstro;

        private List<GameObject> activeAsteroids = new List<GameObject>();
        private List<GameObject> activeUFOs = new List<GameObject>();


        private void Start()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            GameAnalytics.gameAnalytics.InterstitialAd();
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    Debug.Log("Firebase initialized successfully.");
                    FirebaseAnalytics.LogEvent("test_event");
                    Debug.Log("Test event sent.");
                }
                else
                {
                    Debug.LogError($"Could not resolve Firebase dependencies: {task.Result}");
                }
            });
            RunAsyncMethods();
        }


        private async void RunAsyncMethods()
        {
            await StartAsteroidSpawning();
            await StartUFOSpawning();
        }

        private async Task StartAsteroidSpawning()
        {
            AdsService.Instance.bannerAds.ShowBannerAd();
            await Task.Delay(1000);

            while (true)
            {
                if (activeAsteroids.Count < maxAsteroids)
                {
                    SpawnAsteroid();
                }

                await Task.Delay(5000);
            }
        }

        private async UniTask StartUFOSpawning()
        {
            await UniTask.Delay(3000);
            AdsService.Instance.bannerAds.HideBannerAd();
            while (true)
            {
                if (activeUFOs.Count < maxUFOs)
                {
                    SpawnUfo();
                }

                await UniTask.Delay(5000);
            }
        }

        private void SpawnAsteroid()
        {
            GameObject asteroid = poolAstro.GetFromPool(EnemyType.Large);
            Transform spawnPoint = GetRandomSpawnPoint();
            asteroid.transform.position = spawnPoint.position;

            activeAsteroids.Add(asteroid);

            asteroid.GetComponent<Enemy>().OnDestroyed += HandleAsteroidDestroyed;
        }

        private void SpawnUfo()
        {
            GameObject ufo = poolAstro.GetFromPool(EnemyType.Ufo);
            Transform spawnPoint = GetRandomSpawnPoint();

            ufo.transform.position = spawnPoint.position;

            activeUFOs.Add(ufo);

            ufo.GetComponent<Enemy>().OnDestroyed += HandleUFODestroyed;
        }

        private void HandleAsteroidDestroyed(GameObject asteroid)
        {
            activeAsteroids.Remove(asteroid);
        }

        private void HandleUFODestroyed(GameObject ufo)
        {
            activeUFOs.Remove(ufo);
        }

        private Transform GetRandomSpawnPoint()
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            return spawnPoints[randomIndex];
        }
    }
}