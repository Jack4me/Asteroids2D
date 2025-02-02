using UnityEngine;

namespace Core.StaticData
{
    public interface IStaticDataService
    {
        void LoadStaticData();
        GameObject GetEnemyPrefab(EnemyType enemyType);
    }
}