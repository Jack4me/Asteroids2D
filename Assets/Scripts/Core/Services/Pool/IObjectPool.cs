using UnityEngine;

namespace Core.Services.Pool
{
    public interface IObjectPool
    {
        GameObject GetFromPool(EnemyType enemyType);
        void ReturnToPool(Enemies obj);
    }
}
