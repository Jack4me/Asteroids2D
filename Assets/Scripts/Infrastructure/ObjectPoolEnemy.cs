using System.Collections.Generic;
using Core;
using Core.Factory;
using Core.Intrerfaces;
using Infrastructure.Ref.Services;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ObjectPoolEnemy : IObjectPool
    {
        private List<GameObject> _pool = new List<GameObject>();
        private Transform _poolParent;
        private readonly IGameFactory _gameFactory;

        public ObjectPoolEnemy(Transform poolParent)
        {
            _poolParent = poolParent;
            _gameFactory = AllServices.Container.GetService<IGameFactory>();
        }


        public GameObject GetFromPool(EnemyType enemyType)
        {
            foreach (GameObject obj in _pool)
            {
                if (!obj.activeInHierarchy && obj.GetComponent<Enemy>()?.enemyType == enemyType)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            GameObject newObj = Create(enemyType);
            if (newObj == null)
            {
                Debug.LogError($"Failed to create object of type {enemyType}");
                return null;
            }

            _pool.Add(newObj);
            return newObj;
        }

        private GameObject Create(EnemyType enemyType)
        {
            GameObject newObj = _gameFactory.CreateEnemy(enemyType, _poolParent, this);
            if (newObj == null)
            {
                Debug.LogError($"Factory could not create object of type {enemyType}");
            }

            return newObj;
        }


        public void ReturnToPool(Enemy obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_poolParent);
        }
    }
}