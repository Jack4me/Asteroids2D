using System;
using Core;
using Core.Services;
using Cysharp.Threading.Tasks;
using Game.Entities.Entities.Asteroids;
using Game.Entities.Entities.UFO;
using Game.Handlers.Health;
using Game.Hero;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        public Vector2 Velocity => _velocity;
        [SerializeField] private float _playerBounceForce;
        [SerializeField] private float _asteroidBounceForce;
        [SerializeField] private ParticleSystem _invincibilityEffect;
        [SerializeField] private float _invincibilityDuration;
        [SerializeField] private float _lockDuration;
        [SerializeField] private int _bounceForce = 5;
        private HealthHandler _healthHandler;
        private bool _isInvincible;
        private int _health = 5;
        private Vector2 _velocity;
        private bool _canControl;
        private IBounceService _bounceService;
        private Collider2D _playerCollider;
        public event Action<float> OnControlLockRequested;

        public void Construct(IBounceService bounceService)
        {
            _bounceService = bounceService;
        }

        private void Awake()
        {
            HideInvincibilityEffect();
            _healthHandler = GetComponent<HealthHandler>();
            _playerCollider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isInvincible) return;
            EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();
            if (enemyBullet == null)
            {
                _bounceService.ApplyBounce(transform, other, _bounceForce);
            }

            HandleCollision(other);
        }

        public async void HandleCollision(Collider2D asteroidCollider)
        {
            if (_isInvincible) return;
            _isInvincible = true;
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

            await EnableInvincibility();
            HideInvincibilityEffect();
        }

        private async UniTask EnableInvincibility()
        {
            ShowInvincibilityEffect();
            await GetComponent<HeroBlink>().StartBlinking();
            _isInvincible = true;
            await UniTask.Delay((int)(_invincibilityDuration * 1000));
            HideInvincibilityEffect();
            _isInvincible = false;
            _playerCollider.enabled = true;
        }

        private void ShowInvincibilityEffect()
        {
            if (_invincibilityEffect != null)
            {
                _invincibilityEffect.gameObject.SetActive(true);
                _invincibilityEffect.Play();
            }
        }

        private void HideInvincibilityEffect()
        {
            _invincibilityEffect.Stop();
            _invincibilityEffect.gameObject.SetActive(false);
        }
    }
}