using System.Collections.Generic;
using Core.StaticData;
using UnityEngine;

namespace Core.Services
{
    public class StaticDataService : IStaticDataService
    {
        private UnitConfig _unitConfig;
        private Dictionary<EnemyType, GameObject> _enemyPrefabs;

        public void LoadStaticData()
        {
            _enemyPrefabs = new Dictionary<EnemyType, GameObject>
            {
                { EnemyType.Large, Resources.Load<GameObject>("Prefabs/Enemy/Asteroids/AsteroidBig") },
                { EnemyType.Medium, Resources.Load<GameObject>("Prefabs/Enemy/Asteroids/AsteroidMedium") },
                { EnemyType.Small, Resources.Load<GameObject>("Prefabs/Enemy/Asteroids/AsteroidSmall") },
                { EnemyType.Ufo, Resources.Load<GameObject>("Prefabs/Enemy/UFO/UFO") }
            };
        }

        public GameObject GetEnemyPrefab(EnemyType enemyType)
        {
            if (_enemyPrefabs.TryGetValue(enemyType, out var prefab))
            {
                return prefab;
            }

            Debug.LogError($"Prefab for enemy type {enemyType} not found.");
            return null;
        }
    }
}