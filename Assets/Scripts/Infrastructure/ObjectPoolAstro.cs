using System.Collections.Generic;
using Core.Intrerfaces;
using UnityEngine;

namespace Infrastructure
{
    public class ObjectPoolAstro
    {
        private List<GameObject> _pool = new List<GameObject>();
        private Transform poolParent;
        private readonly IObjectFactory factory;

        public ObjectPoolAstro(Transform poolParent, IObjectFactory factory)
        {
            this.poolParent = poolParent;
            this.factory = factory;
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

            if (prefab == null)
            {
                Debug.Log("NULL HERE");
            }

            GameObject newObj = Create(prefab);


            if (prefab == null)
            {
                Debug.Log("NULL PREFAB in GetFromPool");
                return null; // Если префаб равен null, возвращаем null
            }

            _pool.Add(newObj); // Добавляем новый объект в пул
            return newObj;
        }

        private GameObject Create(GameObject prefab)
        {
            GameObject newObj = null;

            if (prefab == factory.GetAsteroidPrefab())
            {
                newObj = factory.CreateAsteroid(Vector2.zero, new Vector2(1, 0), 5f, poolParent);
            }

            if (prefab == factory.GetMediumAsteroidPrefab())
            {
                newObj = factory.CreateMediumAsteroid(Vector2.zero, new Vector2(1, 0), 5f, poolParent);
            }

            if (prefab == factory.GetSmallAsteroidPrefab())
            {
                newObj = factory.CreateSmallAsteroid(Vector2.zero, new Vector2(1, 0), 5f, poolParent);
            }
            else if (prefab == factory.GetUfoPrefab())
            {
                newObj = factory.CreateUFO(Vector2.zero, new Vector2(1, 0), 5f, poolParent);
            }

            if (newObj == null)
            {
                Debug.LogError("Failed to create object, prefab not found.");
            }
            else
            {
                Debug.Log($"Object created successfully: {newObj.name}");
            }

            return newObj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(poolParent);
        }
    }
}