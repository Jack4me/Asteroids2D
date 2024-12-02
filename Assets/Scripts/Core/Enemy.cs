using System;
using Core.Intrerfaces;
using UnityEngine;
using Zenject;

namespace Core
{
    public enum AsteroidSize
    {
        Small,
        Medium,
        Large, 
        Ufo
    }
    public class Enemy : MonoBehaviour, IDamageable, IHit
    {
        [field: SerializeField] public int Damage { get; set; }
        [SerializeField] protected int health = 1;
        public IObjectPool _pool;
        public event Action<GameObject> OnDestroyed;
        public Enemy(IObjectPool objectPool)
        {
            _pool = objectPool;
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
            OnDestroyed?.Invoke(gameObject);

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

        [SerializeField] private int score = 10;

        public int GetScore() => score;

        
    }
}