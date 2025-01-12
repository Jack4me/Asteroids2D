using Core.Intrerfaces;
using Core.Models;
using UnityEngine;

namespace Core.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud(LaserViewModel laserViewModel);
        void LoadConfigs();
        GameObject CreateEnemy(EnemyType enemyType, Transform poolContainer, IObjectPool objectPoolAstro);
        Transform CreatePoolParent();
        void CleanUp();
    }
}