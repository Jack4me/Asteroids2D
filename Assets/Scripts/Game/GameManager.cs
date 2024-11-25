using Core.Intrerfaces;
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
     
        // Mover mover = asteroid.GetComponent<Mover>();
       // mover.Initialize(new Vector2(1, 0), 5f);
    }
    private void SpawnUFO()
    {
        GameObject asteroid = poolAstro.GetFromPool(ufoPrefab);
     
        // Mover mover = asteroid.GetComponent<Mover>();
        // mover.Initialize(new Vector2(1, 0), 5f);
    }
    private void DestroyAsteroid(GameObject asteroid)
    {
        poolAstro.ReturnToPool(asteroid);
    }
}