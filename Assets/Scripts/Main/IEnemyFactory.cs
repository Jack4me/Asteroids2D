using Core;
using Core.Intrerfaces;
using Core.Services;
using Core.StaticData;
using UnityEngine;

namespace Main
{
    public interface IEnemyFactory
    {
        GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro,
            IStaticDataService staticDataService, GameConfigs gameConfigs, IScorable scoreManager);
    }
}