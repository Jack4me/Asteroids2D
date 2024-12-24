using CodeBase.Infrastructure.StaticData;
using Core.Services;
using UnityEngine;

namespace Core.StaticData {
    public interface IStaticDataService : IService {
        void LoadStaticData();
        UnitConfig GetUnitConfig();
        GameObject GetEnemyPrefab(EnemyType enemyType);
    }
}