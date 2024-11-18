using UnityEngine;
using Zenject;

namespace Infrastructure {
    public class LocationInstaller : MonoInstaller {
        [SerializeField] private Transform starPoint;
        [SerializeField] private GameObject playerPrefab;

        public override void InstallBindings() {
         //   Container.InstantiatePrefabForComponent<>()
        }
    }
}