using CodeBase.Infrastructure.Services;
using Core.Intrerfaces;
using UnityEngine;

namespace Core.Factory {
    public interface IGameFactory : IService {
        GameObject CreateHero(GameObject at);

        GameObject CreateHud();
        void CleanUp();
        void LoadConfigs();
        GameObject CreateEnemy(EnemyType enemyType, Transform pool);
        GameObject CreateEnemy(EnemyType enemyType, Transform poolParent, IObjectPool objectPoolAstro);
    }
}