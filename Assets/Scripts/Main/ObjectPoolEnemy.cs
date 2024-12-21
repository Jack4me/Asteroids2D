using System.Collections.Generic;
using Core;
using Core.Factory;
using Core.Intrerfaces;
using Game;
using Infrastructure.Ref.Services;
using UnityEngine;

namespace Main
{
    public class ObjectPoolEnemy : IObjectPool
    {
        private const string POOL_PARENT = "PoolParent";

        private List<GameObject> _pool = new List<GameObject>();
        private Transform _poolContainer;
        private readonly IGameFactory _gameFactory;

        public ObjectPoolEnemy()
        {
            _gameFactory = AllServices.Container.GetService<IGameFactory>();
            //  _poolContainer = _gameFactory.CreatePoolParent();
        }


        public GameObject GetFromPool(EnemyType enemyType)
        {
            if (_poolContainer == null)
            {
                _poolContainer = GameObject.FindWithTag(POOL_PARENT).transform;
                Debug.Log("POOL NULL");
            }

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

       

        public void ReturnToPool(Enemy obj)
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