using System.Collections.Generic;
using Core;
using Core.Intrerfaces;
using Cysharp.Threading.Tasks;
using Infrastructure;
using UnityEngine;
using Zenject;
using Enemy = Core.Enemy;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject asteroidPrefab;

        [SerializeField] private GameObject ufoPrefab;

        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private int maxAsteroids = 10;
        [SerializeField] private int maxUFOs = 3;
        [Inject] private IObjectPool poolAstro;

        private List<GameObject> activeAsteroids = new List<GameObject>();
        private List<GameObject> activeUFOs = new List<GameObject>();

        private void Start()
        {
            StartAsteroidSpawning().Forget();
            StartUFOSpawning().Forget();
        }

        private async UniTaskVoid StartAsteroidSpawning()
        {
            await UniTask.Delay(5000); 

            while (true)
            {
                if (activeAsteroids.Count < maxAsteroids)
                {
                    SpawnAsteroid();
                }

                await UniTask.Delay(15000); // Ждём 15 секунд
            }
        }

        private async UniTaskVoid StartUFOSpawning()
        {
            await UniTask.Delay(10000); // Ждём 10 секунд
            while (true)
            {
                if (activeUFOs.Count < maxUFOs)
                {
                    SpawnUfo();
                }

                await UniTask.Delay(10000); // Ждём 10 секунд
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