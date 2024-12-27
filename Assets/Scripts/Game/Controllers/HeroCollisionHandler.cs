using System;
using Core;
using Core.Services;
using Cysharp.Threading.Tasks;
using Game.Entities.Entities.Asteroids;
using Game.Entities.Entities.UFO;
using Game.Handlers.Health;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        public Vector2 Velocity => velocity;
        [SerializeField] private float playerBounceForce = 30;
        [SerializeField] private float asteroidBounceForce = 30;
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private ParticleSystem invincibilityEffect;
        [SerializeField] private float invincibilityDuration = 3;
        [SerializeField] private float lockDuration = 1;

        private HealthHandler _healthHandler;
        private bool isInvincible;
        private int health = 5;
        private Vector2 velocity;
        private bool canControl;
        private HeroMove movementController;
        private IBounceService _bounceService;
        private Collider2D _playerCollider;


        public event Action<float> OnControlLockRequested;

        public void Construct(IBounceService bounceService)
        {
            _bounceService = bounceService;
        }

        private void Awake()
        {
            _healthHandler = GetComponent<HealthHandler>();
            movementController = GetComponent<HeroMove>();
            _playerCollider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isInvincible) return;
            EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();

            if (enemyBullet == null)
            {
                _bounceService.ApplyBounce(transform, other, 5);
            }

            HandleCollision(other);
        }

        public void HandleCollision(Collider2D asteroidCollider)
        {
            if (isInvincible) return;
            isInvincible = true;
            if (asteroidCollider.TryGetComponent<IHit>(out var enemy))
            {
                int damage = enemy.Damage;

                _healthHandler.TakeDamage(damage);
                OnControlLockRequested?.Invoke(lockDuration);
            }

            _playerCollider.enabled = false;
            Vector2 collisionDirection =
                (Vector2)transform.position - (Vector2)asteroidCollider.transform.position;
            collisionDirection.Normalize();

            velocity += collisionDirection * playerBounceForce;


            if (asteroidCollider.TryGetComponent<BounceController>(out var bounce))
            {
                bounce.ApplyBounce(-collisionDirection * asteroidBounceForce);
            }

            EnableInvincibility().Forget();
            ShowInvincibilityEffect();
        }


        private async UniTask EnableInvincibility()
        {
            isInvincible = true;
            ShowInvincibilityEffect();

            await UniTask.Delay((int)(invincibilityDuration * 1000));
            isInvincible = false;
            _playerCollider.enabled = true;

            HideInvincibilityEffect();
        }

        private void ShowInvincibilityEffect()
        {
            if (invincibilityEffect != null) invincibilityEffect.Play();
        }

        private void HideInvincibilityEffect()
        {
            if (invincibilityEffect != null) invincibilityEffect.Stop();
        }
    }
}