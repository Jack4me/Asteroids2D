using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Core.AssetsManagement {
    public interface IInstantiateProvider : IService {
        GameObject Instantiate(string Path, Vector3 At);
        GameObject Instantiate(string Path);
        GameObject InstantiateToPool(GameObject prefab, Transform pool);
    }
}