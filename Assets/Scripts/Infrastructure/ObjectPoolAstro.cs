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
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

         
            GameObject newObj = null;
            if (prefab == factory.GetAsteroidPrefab())
            {
                newObj = factory.CreateAsteroid(Vector2.zero, new Vector2(1, 0), 5f, poolParent);

                Debug.Log("CREATE ASTEROID BIG");
            }

            if (prefab == factory.GetMediumAsteroidPrefab())
            {
                newObj = factory.CreateMediumAsteroid(Vector2.zero, new Vector2(1, 0), 5f, poolParent);
            }
            else if (prefab == factory.GetUfoPrefab())
            {
                newObj = factory.CreateUFO(Vector2.zero, new Vector2(1, 0), 5f, poolParent);
            }

            _pool.Add(newObj);
            return newObj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(poolParent);
            _pool.Add(obj);
        }
    }
}