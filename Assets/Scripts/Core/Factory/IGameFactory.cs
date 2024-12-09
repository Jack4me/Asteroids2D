using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Core.Factory {
    public interface IGameFactory : IService {
        GameObject CreateHero(GameObject at);

        GameObject CreateHud();
        void CleanUp();
        void LoadConfigs();
        void CreateUnit();
    }
}