using System;
using Core;
using Core.Services;
using Cysharp.Threading.Tasks;
using Game.Entities.Entities.Asteroids;
using Game.Entities.Entities.UFO;
using Game.Handlers;
using Game.Handlers.Health;
using Game.Hero;
using UnityEngine;
namespace Game.Controllers
{
    public class HeroCollisionHandler : MonoBehaviour
    {
        public Vector2 Velocity => _velocity;
        [SerializeField] private float _playerBounceForce;
        [SerializeField] private float _asteroidBounceForce;
       
        [SerializeField] private float _lockDuration;
        [SerializeField] private int _bounceForce = 5;
        private HealthHandler _healthHandler;
        private int _health = 5;
        private Vector2 _velocity;
        private bool _canControl;
        private IBounceService _bounceService;
        private Collider2D _playerCollider;
        private InvincibilityHandler _invincibilityHandler;
        public event Action<float> OnControlLockRequested;

        public void Construct(IBounceService bounceService)
        {
            _bounceService = bounceService;
            
        }

        private void Awake()
        {
            _healthHandler = GetComponent<HealthHandler>();
            _playerCollider = GetComponent<Collider2D>();
            _invincibilityHandler = GetComponent<InvincibilityHandler>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_invincibilityHandler._isInvincible) return;
            EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();
            if (enemyBullet == null)
            {
                _bounceService.ApplyBounce(transform, other, _bounceForce);
            }

            HandleCollision(other);
        }

        public async void HandleCollision(Collider2D asteroidCollider)
        {
            if (_invincibilityHandler._isInvincible) return;
            _invincibilityHandler.SetInvincibileTrue();
            if (asteroidCollider.TryGetComponent<IHit>(out var enemy))
            {
                int damage = enemy.Damage;
                _healthHandler.TakeDamage(damage);
                OnControlLockRequested?.Invoke(_lockDuration);
            }

            _playerCollider.enabled = false;
            Vector2 collisionDirection =
                (Vector2)transform.position - (Vector2)asteroidCollider.transform.position;
            collisionDirection.Normalize();
            _velocity += collisionDirection * _playerBounceForce;
            if (asteroidCollider.TryGetComponent<BounceController>(out var bounce))
            {
                bounce.ApplyBounce(-collisionDirection * _asteroidBounceForce);
            }

            await _invincibilityHandler.EnableInvincibility();
            _invincibilityHandler.HideInvincibilityEffect();
        }

       
    }
}