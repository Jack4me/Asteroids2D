using System.Collections.Generic;
using Core;
using Core.Factory;
using Core.Services;
using Core.Services.Pool;
using UnityEngine;

namespace Infrastructure.Init
{
    public class ObjectPoolEnemy : IObjectPool
    {
        private const string POOL_PARENT = "PoolParent";

        private List<GameObject> _pool = new List<GameObject>();
        private Transform _poolContainer;
        private readonly IGameFactory _gameFactory;

        public ObjectPoolEnemy(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _poolContainer = _gameFactory.CreatePoolParent();
        }


        public GameObject GetFromPool(EnemyType enemyType)
        {
            _poolContainer = GameObject.FindWithTag(POOL_PARENT).transform;


            foreach (GameObject obj in _pool)
            {
                if (!obj.activeInHierarchy && obj.GetComponent<Enemies>()?.enemyType == enemyType)
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

       

        public void ReturnToPool(Enemies obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_poolContainer);
        }

        private GameObject Create(EnemyType enemyType)
        {
            GameObject newObj = _gameFactory.CreateEnemy(enemyType, _poolContainer, this);
            if (newObj == null)
            {
                Debug.LogError($"Factory could not create object of type {enemyType}");
            }

            return newObj;
        }
    }
}