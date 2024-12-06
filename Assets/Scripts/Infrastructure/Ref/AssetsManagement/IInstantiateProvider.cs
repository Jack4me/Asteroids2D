using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetsManagement {
    public interface IInstantiateProvider : IService {
        GameObject Instantiate(string Path, Vector3 At);
        GameObject Instantiate(string Path);
        GameObject Instantiate(GameObject prefab);
    }
}