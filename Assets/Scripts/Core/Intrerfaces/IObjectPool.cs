using UnityEngine;

namespace Core.Intrerfaces
{
    public   interface IObjectPool
    {
        GameObject GetFromPool(GameObject obj);
        void ReturnToPool(Entity obj);
    }
}
