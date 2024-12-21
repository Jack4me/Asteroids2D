using CodeBase.Infrastructure.Services;
using UniRx.Examples;
using UnityEngine;
using Enemy = Core.Enemy;

namespace Core.Intrerfaces
{
    public interface IObjectPool : IService
    {
        GameObject GetFromPool(EnemyType enemyType);
        void ReturnToPool(Enemy obj);
    }
}
