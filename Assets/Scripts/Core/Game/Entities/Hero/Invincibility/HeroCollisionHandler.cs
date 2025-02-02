using System;
using Core.Game.Handlers.Health;
using Core.Services;
using Game.Entities.Entities;
using Game.Entities.Entities.Asteroids;
using UnityEngine;

namespace Core.Game.Entities.Hero.Invincibility
{
    public class HeroCollisionHandler : MonoBehaviour
    {
         public Vector2 Velocity => _velocity;

        [SerializeField] private float _playerBounceForce;
        [SerializeField] private float _asteroidBounceForce;
        [SerializeField] private float _lockDuration;
        [SerializeField] private int _bounceForce = 5;

        private HealthHandler _healthHandler;
        private InvincibilityHandler _invincibilityHandler;
        private Collider2D _playerCollider;
        private IBounceService _bounceService;

        private Vector2 _velocity;
        private bool _canControl;

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
            if (_invincibilityHandler.IsInvincible) return;

            if (other.TryGetComponent<UFOBullet>(out var bullet))
            {
                HandleBulletCollision(bullet);
            }
            else if (other.TryGetComponent<IHit>(out var enemy))
            {
                HandleEnemyCollision(other, enemy);
                HandleBounce(other);
            }
            else
            {
                HandleBounce(other);
            }
        }

        private void HandleBulletCollision(UFOBullet bullet)
        {
            _healthHandler.TakeDamage(bullet.Damage);
        }

        private async void HandleEnemyCollision(Collider2D collider, IHit enemy)
        {
            _invincibilityHandler.SetInvincibile(true);
            _healthHandler.TakeDamage(enemy.Damage);
            OnControlLockRequested?.Invoke(_lockDuration);

            DisablePlayerCollider();
            ApplyBounceFromCollision(collider);
            await _invincibilityHandler.EnableInvincibility();
            _invincibilityHandler.HideInvincibilityEffect();
        }

        private void HandleBounce(Collider2D collider)
        {
            _bounceService.ApplyBounce(transform, collider, _bounceForce);
        }

        private void DisablePlayerCollider()
        {
            _playerCollider.enabled = false;
        }

        private void ApplyBounceFromCollision(Collider2D collider)
        {
            Vector2 collisionDirection = ((Vector2)transform.position - (Vector2)collider.transform.position).normalized;
            _velocity += collisionDirection * _playerBounceForce;

            if (collider.TryGetComponent<BounceController>(out var bounce))
            {
                bounce.ApplyBounce(-collisionDirection * _asteroidBounceForce);
            }
        }

       
    }
}