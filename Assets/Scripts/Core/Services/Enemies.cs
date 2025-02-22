using Core.Intrerfaces;
using Core.Services.Pool;
using UnityEngine;

namespace Core.Services
{
    public class Enemies : MonoBehaviour, IDamageable, IHit, IStatsEnemy
    {
        [SerializeField] private int _bounceForce = 5;
        public IObjectPool _pool;
        public EnemyType enemyType;
        private IScorable _score;
        private IBounceService _bounceService;
        private int _force;
        public int Damage { get; set; }
        public int Health { get; set; }
        public float Speed { get; set; }

        public void Initialize(IObjectPool objectPool, IScorable score, IBounceService bounceService)
        {
            _pool = objectPool;
            _score = score;
            _bounceService = bounceService;
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
        }

        public void ReturnToPool()
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ILaserController _))
            {
                _bounceService.ApplyBounce(transform, other, _bounceForce);
            }
        }
    }
}