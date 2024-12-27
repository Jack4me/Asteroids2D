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
        [SerializeField] private float _playerBounceForce = 30;
        [SerializeField] private float _asteroidBounceForce = 30;
        [SerializeField] private ParticleSystem _invincibilityEffect;
        [SerializeField] private float _invincibilityDuration = 3;
        [SerializeField] private float _lockDuration = 1;

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
                _bounceService.ApplyBounce(transform, other, 5);
            }

            HandleCollision(other);
        }

        public void HandleCollision(Collider2D asteroidCollider)
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

            EnableInvincibility().Forget();
           
        }


        private async UniTask EnableInvincibility()
        {
            ShowInvincibilityEffect();
            await GetComponent<HeroBlink>().StartBlinking();

            _isInvincible = true;

            await UniTask.Delay((int)(_invincibilityDuration * 1000));
            _isInvincible = false;
            _playerCollider.enabled = true;

            HideInvincibilityEffect();
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
            if (_invincibilityEffect != null)
            {
                _invincibilityEffect.Stop();
                _invincibilityEffect.gameObject.SetActive(false);
            }
        }
    }
}