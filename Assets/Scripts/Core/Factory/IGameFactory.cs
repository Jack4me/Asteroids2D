using CodeBase.Infrastructure.Services;
using Core.Intrerfaces;
using UnityEngine;

namespace Core.Factory {
    public interface IGameFactory : IService {
        GameObject CreateHero(GameObject at);

        GameObject CreateHud();
        void CleanUp();
        void LoadConfigs();
        GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro);
        Transform CreatePoolParent();
    }
}