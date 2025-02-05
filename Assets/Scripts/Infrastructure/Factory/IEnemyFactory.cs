using Core;
using Core.Services.Pool;
using Core.StaticData;
using Core.StaticData.Configs;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IEnemyFactory
    {
        GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro,
            IStaticDataService staticDataService, UFOConfig heroMoveConfig);
    }
}