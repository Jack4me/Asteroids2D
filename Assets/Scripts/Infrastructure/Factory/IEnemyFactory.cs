using Core;
using Core.Services;
using Core.Services.Pool;
using Core.StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IEnemyFactory
    {
        GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro,
            IStaticDataService staticDataService, UFOConfig heroMoveConfig);
    }
}