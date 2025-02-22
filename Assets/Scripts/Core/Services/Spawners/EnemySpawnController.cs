﻿using Core.Services.Pool;
using Core.Services.Spawners;
using Core.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services
{
    public class EnemySpawnController : ISpawnService
    {
        public readonly IObjectPool _pool;
        private Vector3 spawnPosition;
        private AsteroidSpawner _asteroidSpawner;
        private UfoSpawner _ufoSpawner;

        public EnemySpawnController(IObjectPool pool)
        {
            _pool = pool;
        }

        public async void RunAsyncMethods(SpawnPointsData spawnPointsData)
        {
            _asteroidSpawner = new AsteroidSpawner(_pool);
            _ufoSpawner = new UfoSpawner(_pool);
            await UniTask.WhenAll(
                _ufoSpawner.StartAsyncUFOSpawning(spawnPointsData),
                _asteroidSpawner.StartAsyncAsteroidSpawning(spawnPointsData));
        }

        public Vector3 GetRandomSpawnPoint(SpawnPointsData spawnPointsData)
        {
            if (spawnPointsData != null && spawnPointsData.spawnPositions.Length > 0)
            {
                spawnPosition = spawnPointsData.spawnPositions[Random.Range(0, spawnPointsData.spawnPositions.Length)];
            }
            else
            {
                Debug.LogError("Dont have SpawnPoints");
            }

            return spawnPosition;
        }
    }
}