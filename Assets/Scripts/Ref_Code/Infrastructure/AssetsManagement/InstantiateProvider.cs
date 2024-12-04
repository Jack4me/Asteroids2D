using UnityEngine;

namespace CodeBase.Infrastructure.AssetsManagement {
    public class InstantiateProvider : IInstantiateProvider {
        public GameObject Instantiate(string path){
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(GameObject prefab){
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at){
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}