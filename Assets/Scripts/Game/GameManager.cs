using Core.Intrerfaces;
using Game.Entities.Entities;
using Infrastructure;
using UnityEngine;
using ObjectFactory = Infrastructure.Factories.ObjectFactory;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject mediumAsteroid;
    [SerializeField] private GameObject smallAsteroid;
    [SerializeField] private GameObject ufoPrefab;
    [SerializeField] private Transform poolParent;

    
    
    private IObjectFactory _factory;
    private ObjectPoolAstro poolAstro;

    private void Awake()
    {
        _factory = new ObjectFactory(asteroidPrefab, ufoPrefab, mediumAsteroid, smallAsteroid);
        poolAstro = new ObjectPoolAstro(poolParent, _factory);
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        GameObject asteroid = poolAstro.GetFromPool(asteroidPrefab);

        var asteroidScript = asteroid.GetComponent<Entity>();
        asteroidScript.Initialize(poolAstro); 
    }

    private void SpawnUFO()
    {
        GameObject asteroid = poolAstro.GetFromPool(ufoPrefab);
    }

    private void DestroyAsteroid(GameObject asteroid)
    {
        poolAstro.ReturnToPool(asteroid);
    }
}