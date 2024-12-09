using System.Collections.Generic;
using Core;
using Core.Intrerfaces;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ObjectPoolAstro : IObjectPool
    {
        private List<GameObject> _pool = new List<GameObject>();
        private Transform poolParent;
        private readonly IObjectFactory factory;
        private ScoreManager scoreManager;
        public ObjectPoolAstro(Transform poolParent, IObjectFactory factory)
        {
            this.poolParent = poolParent;
            this.factory = factory;
        }
        [Inject]
        public void Construct(ScoreManager scoreManager)
        {
            this.scoreManager = scoreManager;
        }
        public GameObject GetFromPool(GameObject prefab)
        {
            foreach (GameObject obj in _pool)
            {
                if (!obj.activeInHierarchy && obj.name.StartsWith(prefab.name)) // Проверяем имя для соответствия
                {
                    obj.SetActive(true); // Активируем объект
                    return obj; // Возвращаем найденный объект
                }
            }


            GameObject newObj = Create(prefab);


            if (prefab == null)
            {
                Debug.Log("NULL PREFAB in GetFromPool");
                return null;
            }

            _pool.Add(newObj);
            return newObj;
        }

        private GameObject Create(GameObject prefab)
        {
            GameObject newObj = null;

            if (prefab == factory.GetAsteroidPrefab())
            {
                newObj = factory.CreateAsteroid(Vector2.zero, new Vector2(1, 0), 5f, poolParent, this, scoreManager);
            }

            if (prefab == factory.GetMediumAsteroidPrefab())
            {
                newObj = factory.CreateMediumAsteroid(Vector2.zero, new Vector2(1, 0), 5f, poolParent, this, scoreManager);
            }

            if (prefab == factory.GetSmallAsteroidPrefab())
            {
                newObj = factory.CreateSmallAsteroid(Vector2.zero, new Vector2(1, 0), 5f, poolParent, this, scoreManager);
            }
            else if (prefab == factory.GetUfoPrefab())
            {
                newObj = factory.CreateUFO(Vector2.zero, new Vector2(1, 0), 5f, poolParent, this, scoreManager);
            }

            

            return newObj;
        }


        public void ReturnToPool(Enemy obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(poolParent);
        }
    }
}