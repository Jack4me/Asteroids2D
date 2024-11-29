using System.Collections.Generic;
using Core;
using Core.Intrerfaces;
using Cysharp.Threading.Tasks;
using Game.Entities.Entities;
using Infrastructure;
using UnityEngine;
using Zenject;
using ObjectFactory = Infrastructure.Factories.ObjectFactory;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject mediumAsteroid;
    [SerializeField] private GameObject smallAsteroid;
    [SerializeField] private GameObject ufoPrefab;
    [SerializeField] private Transform poolParent;
    [SerializeField] private int initialAsteroidCount = 5;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxAsteroids = 10;
    [SerializeField] private int maxUFOs = 3;
    [Inject] private ObjectPoolAstro poolAstro;
    
    private List<GameObject> activeAsteroids = new List<GameObject>();
    private List<GameObject> activeUFOs = new List<GameObject>();

    private void Start()
    {
        StartAsteroidSpawning().Forget();
        StartUFOSpawning().Forget();
    }
    private async UniTaskVoid StartAsteroidSpawning()
    {
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
        GameObject asteroid = poolAstro.GetFromPool(asteroidPrefab);
        Transform spawnPoint = GetRandomSpawnPoint();
        asteroid.transform.position = spawnPoint.position;

        activeAsteroids.Add(asteroid);

        asteroid.GetComponent<Entity>().OnDestroyed += HandleAsteroidDestroyed;
    }
    
    private void SpawnUfo()
    {
        GameObject ufo = poolAstro.GetFromPool(ufoPrefab);
        Transform spawnPoint = GetRandomSpawnPoint();

        ufo.transform.position = spawnPoint.position;

        activeUFOs.Add(ufo);

        ufo.GetComponent<Entity>().OnDestroyed += HandleUFODestroyed;
       
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