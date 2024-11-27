using Core.Intrerfaces;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Entities.Entities
{
    public class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int health = 1;
        [Inject] internal ObjectPoolAstro _pool;

        //internal ObjectPoolAstro _pool; 

        // public void Initialize(ObjectPoolAstro pool)
        // {
        //     _pool = pool;
        // }
        public virtual void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                 DestroyEntity();
            }
        }

        public virtual void DestroyEntity()
        {
            ReturnToPool();
        }

        public virtual void ReturnToPool()
        {
            if (_pool != null)
            {
                _pool.ReturnToPool(gameObject);
            }
            else
            {
                Debug.LogError("Object pool is not assigned for this entity!");
                Destroy(gameObject); // Если пула нет, удаляем объект
            }
        }
    }
}