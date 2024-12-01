using System;
using Core.Intrerfaces;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Entity : MonoBehaviour, IDamageable, IHit
    {
        [SerializeField] protected int health = 1;
        public IObjectPool _pool;
        [field: SerializeField] public int Damage { get; set; }
        public event Action<GameObject> OnDestroyed;
        public Entity(IObjectPool objectPool)
        {
            _pool = objectPool;
        }



        public virtual void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                DestroyEntity();
                OnDestroyed?.Invoke(gameObject);
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