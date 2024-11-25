using Core.Intrerfaces;
using Game.Entities.Entities.Asteroids;
using Infrastructure;
using UnityEngine;
using ObjectFactory = Infrastructure.Factories.ObjectFactory;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject ufoPrefab;
    [SerializeField] private Transform poolParent;

    private IObjectFactory _factory;
    private ObjectPoolAstro poolAstro;

    private void Awake()
    {
        _factory = new ObjectFactory(asteroidPrefab, ufoPrefab);

        // Создаём пул с фабрикой
        poolAstro = new ObjectPoolAstro(poolParent, _factory);
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnAsteroid();
            SpawnUFO();
        }
    }

    private void SpawnAsteroid()
    {
        GameObject asteroid = poolAstro.GetFromPool(asteroidPrefab);
        
        var asteroidScript = asteroid.GetComponent<Asteroid>();
        asteroidScript.Initialize(poolAstro); // Передаём пул астероидов
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