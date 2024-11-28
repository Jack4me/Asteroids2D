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
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-10, -10);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(10, 10);
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
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject asteroid = poolAstro.GetFromPool(asteroidPrefab);
        asteroid.transform.position = spawnPosition;

       
    }
    
    private void SpawnUfo()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject asteroid = poolAstro.GetFromPool(ufoPrefab);
        asteroid.transform.position = spawnPosition;

       
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        return new Vector2(x, y);
    }
   
}