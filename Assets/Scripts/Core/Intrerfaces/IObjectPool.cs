using Core.Services;
using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IObjectPool : IService
    {
        GameObject GetFromPool(EnemyType enemyType);
        void ReturnToPool(Enemy obj);
    }
}
