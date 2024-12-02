using Game.Entities.Entities;
using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IObjectPool
    {
        GameObject GetFromPool(GameObject obj);
        void ReturnToPool(Enemy obj);
    }
}
