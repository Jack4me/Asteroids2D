﻿using System;
using System.Collections;
using Core.Intrerfaces;
using Cysharp.Threading.Tasks;
using Game.Entities.Entities.Asteroids;
using UnityEngine;
using Zenject;

namespace Game.Controllers {
    public class PlayerController : MonoBehaviour {
        private IControlStrategy controlStrategy;
        private Vector2 velocity;
        private float speed = 5f;
        private const int MaxHealth = 50;
        private float acceleration = 0.1f;
        private float rotationSpeed = 180f;
        [SerializeField] private int health;

        [Header("Fire Settings")] [SerializeField]
        private GameObject laserPrefab;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float laserCooldown = 0.5f;
        [SerializeField] private float bulletCooldown = 0.25f;

        [Header("Invincibility Settings")] [SerializeField]
        private ParticleSystem invincibilityEffect;

        [SerializeField] private float invincibilityDuration = 3f;
        private bool isInvincible = false;

        [Header("Bounce Settings")] [SerializeField]
        private float playerBounceForce = 8f;

        [SerializeField] private float asteroidBounceForce = 5f;

        [SerializeField] private float controlLockDuration = 1f;

        public int Health
        {
            get { return health; }
            private set { health = Mathf.Clamp(value, 0, MaxHealth); }
        }

        [Inject]
        public void Construct(IControlStrategy controlStrategy) {
            this.controlStrategy = controlStrategy;
        }

        private float lastLaserFireTime;
        private float lastBulletFireTime;
        private bool canControl = true;


        private void Update() {
            if (canControl) {
                HandleInput();
                HandleMovement();
            }
        }

        private void HandleInput() {
            if (controlStrategy.FireLaser()) {
                FireLaser();
            }

            if (controlStrategy.FireBullet()) {
                FireBullet();
            }
        }

        public void FireLaser() {
            if (Time.time - lastLaserFireTime < laserCooldown) return;

            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            lastLaserFireTime = Time.time;
        }

        public void FireBullet() {
            if (Time.time - lastBulletFireTime < bulletCooldown) return;

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            lastBulletFireTime = Time.time;
        }

        public void HandleMovement() {
            Vector2 input = controlStrategy.GetInput();

            float rotation = -input.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotation);

            velocity += (Vector2)transform.up * input.y * acceleration;

            if (velocity.magnitude > speed) {
                velocity = velocity.normalized * speed;
            }
            transform.position += (Vector3)velocity * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("BAH");

            if (isInvincible) return;

            if (other.TryGetComponent<IDamageable>(out var asteroid)) {
                HandleCollision(other);
            }
        }


        private void HandleCollision(Collider2D asteroidCollider) {
            Health--;

            AsteroidController asteroid = asteroidCollider.GetComponent<AsteroidController>();
            if (asteroid == null) return;

            Vector2 collisionDirection = (Vector2)transform.position - (Vector2)asteroidCollider.transform.position;
            collisionDirection.Normalize();

            LockControlForSeconds(controlLockDuration).Forget();
            velocity = collisionDirection * playerBounceForce;

            asteroid.ApplyBounce(-collisionDirection * asteroidBounceForce);
            EnableInvincibility().Forget();

            if (Health <= 0) {
                Debug.Log("Player is dead!");
                Destroy(gameObject);
            }
        }

        private async UniTask LockControlForSeconds(float duration) {
            canControl = false; 
            await UniTask.Delay((int)(duration * 1000)); 
            canControl = true; 
        }

        private async UniTask EnableInvincibility() {
            isInvincible = true;
            ShowInvincibilityEffect();

            await UniTask.Delay((int)(invincibilityDuration * 1000));
            isInvincible = false;
            HideInvincibilityEffect();
        }

        private void ShowInvincibilityEffect() {
            if (invincibilityEffect != null) invincibilityEffect.Play();
        }

        private void HideInvincibilityEffect() {
            if (invincibilityEffect != null) invincibilityEffect.Stop();
        }
    }
}