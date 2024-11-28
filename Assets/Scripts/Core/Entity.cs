using Core.Intrerfaces;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int health = 1;
        public IObjectPool _pool;

        public Entity(IObjectPool objectPool)
        {
            _pool = objectPool;
        }
        //internal ObjectPoolAstro _pool; 

        // public void Initialize(ObjectPoolAstro pool)
        // {
        //     _pool = pool;
        // }

        private void Start()
        {
            if (_pool == null)
            {
                Debug.Log("POOL IS NULL");
            }
            else
            {
                Debug.Log("POOL IS ACTIVE");

            }
        }

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
                _pool.ReturnToPool(this);
            }
            else
            {
                Debug.LogError("Object pool is not assigned for this entity!");
                Destroy(gameObject); // Если пула нет, удаляем объект
            }
        }
    }
}