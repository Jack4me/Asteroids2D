using System;
using Core.Intrerfaces;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Enemy : MonoBehaviour, IDamageable, IHit, IStatsEnemy
    {
        public IObjectPool _pool;
        private IScorable _score;
        public EnemyType enemyType;
        public ScoreManager ScoreManager;


        [field: SerializeField] public int Damage { get; set; }
        public int score { get; set; }
        public int Health { get; set; }
        public float Speed { get; set; }

        public event Action<GameObject> OnDestroyed;

        public void Initialize(IObjectPool objectPool, IScorable score)
        {
            _pool = objectPool;
            _score = score;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                DestroyEntity();
            }
        }


        public virtual void DestroyEntity()
        {
            ReturnToPool();
            _score.NotifyEnemyDestroyed(enemyType);
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

        public EnemyType GetNextAsteroidType(EnemyType currentType)
        {
            return currentType switch
            {
                EnemyType.Large => EnemyType.Medium,
                EnemyType.Medium => EnemyType.Small,
                _ => EnemyType.None, // Если нет следующего размера
            };
        }

        public int GetScore() => score;
    }
}