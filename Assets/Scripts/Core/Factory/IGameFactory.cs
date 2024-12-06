using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory {
    public interface IGameFactory : IService {
        GameObject CreateHero(GameObject at);

        GameObject CreateHud();
        void CleanUp();

        void CreateUnit();
    }
}