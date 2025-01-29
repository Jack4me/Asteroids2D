using Core;
using Core.Else;
using Core.Game.Entities.Enemies;
using Core.Intrerfaces;
using Core.StaticData;
using Infrastructure;
using UnityEngine;

namespace Main
{
    public interface IEnemyFactory
    {
        GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPoolAsteroidGame objectPoolAsteroidGameAstro,
            IStaticDataService staticDataService, UFOConfig heroMoveConfig);
    }
}