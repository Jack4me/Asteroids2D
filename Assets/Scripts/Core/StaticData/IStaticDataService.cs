using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace Core.StaticData {
    public interface IStaticDataService : IService {
        void LoadStaticData();
        UnitConfig GetUnitConfig();
        GameObject GetEnemyPrefab(EnemyType enemyType);
    }
}