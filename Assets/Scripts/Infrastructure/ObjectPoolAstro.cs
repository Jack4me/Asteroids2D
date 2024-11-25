using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class ObjectPoolAstro
    {
        private List<GameObject> _pool = new List<GameObject>();
        private Transform poolParent;

        public ObjectPoolAstro(Transform poolParent)
        {
            this.poolParent = poolParent;
        }

        public GameObject GetFromPool(GameObject prefab)
        {
            foreach (var obj in _pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // Если нет доступных объектов, создаем новый.
            var newObj = Object.Instantiate(prefab, poolParent);
            _pool.Add(newObj);
            return newObj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}