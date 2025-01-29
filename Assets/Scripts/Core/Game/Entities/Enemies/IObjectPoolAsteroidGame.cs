using UnityEngine;

namespace Core.Game.Entities.Enemies
{
    public interface IObjectPoolAsteroidGame
    {
        GameObject GetFromPool(EnemyType enemyType);
        void ReturnToPool(Enemy obj);
        
    }
}
