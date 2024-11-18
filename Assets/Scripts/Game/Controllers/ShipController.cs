using Core.Intrerfaces;
using UnityEngine;
using Zenject;

namespace Game.Controllers {
    public class ShipController : MonoBehaviour {
        private IControlStrategy controlStrategy;
        private Vector2 velocity;
        private float speed = 5f;
        private float acceleration = 0.1f;
        private float rotationSpeed = 180f;
        private int health;
        private const int MaxHealth = 3;

        [Header("Fire Settings")] [SerializeField]
        private GameObject laserPrefab;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float laserCooldown = 0.5f;
        [SerializeField] private float bulletCooldown = 0.25f;

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
    }
}