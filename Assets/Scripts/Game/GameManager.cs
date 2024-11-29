using Core;
using Core.Intrerfaces;
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
    
    [Inject] private ObjectPoolAstro poolAstro;
    
    

    private void Start()
    {
        for (int i = 0; i < initialAsteroidCount; i++)
        {
            SpawnAsteroid();
        }
        SpawnUfo();
    }

    private void SpawnAsteroid()
    {
        GameObject asteroid = poolAstro.GetFromPool(asteroidPrefab);
        Transform spawnPoint = GetRandomSpawnPoint();
        asteroid.transform.position = spawnPoint.position;

       
    }
    
    private void SpawnUfo()
    {
        GameObject asteroid = poolAstro.GetFromPool(ufoPrefab);
        Transform spawnPoint = GetRandomSpawnPoint();

        asteroid.transform.position = spawnPoint.position;

       
    }


    private Transform GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length); 
        return spawnPoints[randomIndex]; 
    }
   
}