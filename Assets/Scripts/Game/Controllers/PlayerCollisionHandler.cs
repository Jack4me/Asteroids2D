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
       // private readonly Transform playerTransform;
        private readonly float playerBounceForce;
        private readonly float asteroidBounceForce;
        private readonly int maxHealth;
        private readonly ParticleSystem invincibilityEffect;
        private readonly float invincibilityDuration;
        private readonly DamageHandler damageHandler;

        private bool isInvincible;
        private int health;
        private Vector2 velocity;
        private bool canControl;
        public event Action<float> OnControlLockRequested;

        public int Health
        {
            get => health;
            private set => health = Mathf.Clamp(value, 0, maxHealth);
        }

        public Vector2 Velocity => velocity;

        public PlayerCollisionHandler(Transform playerTransform,
            int initialHealth,
            int maxHealth,
            float playerBounceForce,
            float asteroidBounceForce,
            ParticleSystem invincibilityEffect,
            float invincibilityDuration, DamageHandler damageHandler)
        {
           // this.playerTransform = playerTransform;
            this.health = initialHealth;
            this.maxHealth = maxHealth;
            this.playerBounceForce = playerBounceForce;
            this.asteroidBounceForce = asteroidBounceForce;
            this.invincibilityEffect = invincibilityEffect;
            this.invincibilityDuration = invincibilityDuration;
            this.damageHandler = damageHandler;
            isInvincible = false;
        }

        public (Vector2 direction, float force) CalculateBounce(Collider2D asteroidCollider)
        {
            Vector2 collisionDirection =
                (Vector2)transform.position - (Vector2)asteroidCollider.transform.position;
            collisionDirection.Normalize();

            return (collisionDirection, playerBounceForce);
        }

        public async UniTask HandleCollision(Collider2D asteroidCollider, float controlLockDuration)
        {
            if (isInvincible) return;
            ShowInvincibilityEffect();
            if (asteroidCollider.TryGetComponent<IHit>(out var enemy))
            {
                int damage = enemy.Damage;
                damageHandler.TakeDamage(damage);
            }

            Vector2 collisionDirection =
                (Vector2)transform.position - (Vector2)asteroidCollider.transform.position;
            collisionDirection.Normalize();

            velocity += collisionDirection * playerBounceForce;
            OnControlLockRequested?.Invoke(controlLockDuration);


            if (asteroidCollider.TryGetComponent<BounceController>(out var bounce))
            {
                bounce.ApplyBounce(-collisionDirection * asteroidBounceForce);
            }

            EnableInvincibility().Forget();
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