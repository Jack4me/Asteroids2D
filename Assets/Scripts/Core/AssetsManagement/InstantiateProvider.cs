using UnityEngine;

namespace Core.AssetsManagement {
    public class InstantiateProvider : IInstantiateProvider {
        public GameObject Instantiate(string path){
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at){
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject InstantiateToPool(GameObject prefab, Transform at){
            return Object.Instantiate(prefab, at);
        }
    }
}