using System;
using Core.Intrerfaces;
using Core.Services;
using UnityEngine;

namespace Core
{
    public class Enemy : MonoBehaviour, IDamageable, IHit, IStatsEnemy
    {
        public IObjectPool _pool;
        public EnemyType enemyType;
        public ScoreService ScoreService;


        private IScorable _score;
        private IBounceService _bounceService;
        [field: SerializeField] public int Damage { get; set; }
        public int score { get; set; }
        public int Health { get; set; }
        public float Speed { get; set; }

        public event Action<GameObject> OnDestroyed;

        public void Initialize(IObjectPool objectPool, IScorable score, IBounceService bounceService)
        {
            _pool = objectPool;
            _score = score;
            _bounceService = bounceService;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out LaserController heroMove))
            {
                _bounceService.ApplyBounce(transform, other, 5);
            }
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
                Destroy(gameObject);
            }
        }

        public EnemyType GetNextAsteroidType(EnemyType currentType)
        {
            return currentType switch
            {
                EnemyType.Large => EnemyType.Medium,
                EnemyType.Medium => EnemyType.Small,
                _ => EnemyType.None,
            };
        }
    }
}