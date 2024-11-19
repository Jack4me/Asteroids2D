using System.Collections;
using Core.Intrerfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Controllers {
    public class PlayerController : MonoBehaviour {
        private IControlStrategy controlStrategy;
        private Vector2 velocity;
        private float speed = 5f;
        private float acceleration = 0.1f;
        private float rotationSpeed = 180f;
        [SerializeField]  private int health;
        private const int MaxHealth = 3;

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


        private void Update() {
            HandleInput();
            HandleMovement();
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

        private void OnCollisionEnter2D(Collision2D collision) {
            if (isInvincible) return;

            if (collision.gameObject.CompareTag("Enemy")) {
                HandleCollision(collision);
            }
        }

        private void HandleCollision(Collision2D collision) {
            Health--;

            Vector2 collisionDirection = (Vector2)transform.position - collision.contacts[0].point;
            velocity = collisionDirection.normalized * speed;

            EnableInvincibility().Forget();

            if (Health <= 0) {
                Debug.Log("Player is dead!");
                Destroy(gameObject);
            }
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