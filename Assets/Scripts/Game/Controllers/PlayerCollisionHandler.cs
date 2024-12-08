using System;
using Core;
using Cysharp.Threading.Tasks;
using Game.Entities.Entities.Asteroids;
using Game.Handlers.Health;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private float playerBounceForce = 30;
        [SerializeField] private float asteroidBounceForce = 30;
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private ParticleSystem invincibilityEffect;
        [SerializeField] private float invincibilityDuration = 3;
        private DamageHandler damageHandler;

        private bool isInvincible;
        private int health = 5;
        private Vector2 velocity;
        private bool canControl;
        private HeroMove movementController;
        [SerializeField] private bool isHandlingCollision;

        public int Health
        {
            get => health;
            private set => health = Mathf.Clamp(value, 0, maxHealth);
        }

        public Vector2 Velocity => velocity;

        public PlayerCollisionHandler()
        {
            isInvincible = false;
        }

        private void Awake()
        {
            damageHandler = GetComponent<DamageHandler>();
            movementController = GetComponent<HeroMove>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var (direction, force) = CalculateBounce(other);
            movementController.AddVelocity(direction, force);

            HandleCollision(other);
        }

        public (Vector2 direction, float force) CalculateBounce(Collider2D asteroidCollider)
        {
            Vector2 collisionDirection =
                (Vector2)transform.position - (Vector2)asteroidCollider.transform.position;
            collisionDirection.Normalize();

            return (collisionDirection, playerBounceForce);
        }

        public void HandleCollision(Collider2D asteroidCollider)
        {
            if (isInvincible || isHandlingCollision) return;
            isInvincible = true;
            if (asteroidCollider.TryGetComponent<IHit>(out var enemy))
            {
                int damage = enemy.Damage;
                Debug.Log("Take dmg COUNT");

                damageHandler.TakeDamage(damage);
            }

            Vector2 collisionDirection =
                (Vector2)transform.position - (Vector2)asteroidCollider.transform.position;
            collisionDirection.Normalize();

            velocity += collisionDirection * playerBounceForce;


            if (asteroidCollider.TryGetComponent<BounceController>(out var bounce))
            {
                Debug.Log("COLLISION COUNT");

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