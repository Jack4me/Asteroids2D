using Core.Else;
using Core.Game.Entities.Enemies;
using Core.Game.Entities.Hero.Laser;
using Core.Intrerfaces;
using Core.Models;
using Infrastructure;
using UnityEngine;

namespace Core.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud(ILaserViewModel laserViewModel);
        void LoadConfigs();
        GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPoolAsteroidGame objectPoolAsteroidGameAstro);
        Transform CreatePoolParent();
        void CleanUp();
    }
}