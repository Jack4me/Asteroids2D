using Core.Else;
using Core.Game.Entities.Enemies;
using Core.Intrerfaces;
using Core.Services;
using UnityEngine;

namespace Core
{
    public class Enemy : MonoBehaviour, IDamageable, IHit, IStatsEnemy
    {
        public IObjectPoolAsteroidGame PoolAsteroidGame;
        public EnemyType enemyType;
        [SerializeField] private int _bounceForce = 5;
        private IScorable _score;
        private IBounceService _bounceService;
        private int _force;
        public int Damage { get; set; }
        public int Health { get; set; }
        public float Speed { get; set; }

        public void Initialize(IObjectPoolAsteroidGame objectPoolAsteroidGame, IScorable score, IBounceService bounceService)
        {
            PoolAsteroidGame = objectPoolAsteroidGame;
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

        public virtual void ReturnToPool()
        {
            if (PoolAsteroidGame != null)
            {
                PoolAsteroidGame.ReturnToPool(this);
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
            if (other.TryGetComponent(out ILaserController _heroMove))
            {
                _bounceService.ApplyBounce(transform, other, _bounceForce);
            }
        }
    }
}