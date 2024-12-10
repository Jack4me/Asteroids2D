using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IObjectPool
    {
        GameObject GetFromPool(EnemyType enemyType);
        void ReturnToPool(Enemy obj);
    }
}
