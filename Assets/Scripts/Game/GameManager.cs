using System;
using Core.Intrerfaces;
using Game;
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
        poolAstro = new ObjectPoolAstro(poolParent);
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
        var asteroid = poolAstro.GetFromPool(asteroidPrefab);
       var mover = asteroid.GetComponent<Mover>();
       mover.Initialize(new Vector2(1, 0), 5f);
    }

    private void DestroyAsteroid(GameObject asteroid)
    {
        poolAstro.ReturnToPool(asteroid);
    }
}