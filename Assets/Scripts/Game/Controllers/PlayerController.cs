using System;
using Core.Intrerfaces;
using Cysharp.Threading.Tasks;
using Game.Entities.Entities.Asteroids;
using Game.Handlers.Health;
using Game.Models;
using UnityEngine;
using Zenject;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovementController movementController;
        private PlayerShootingController shootingController;
        private PlayerCollisionHandler collisionHandler;

        private LaserManager laserManager;
        private IControlStrategy controlStrategy;
        private Vector2 velocity;
        private float speed = 5f;
        private const int MaxHealth = 10;
        private float acceleration = 0.1f;
        private float rotationSpeed = 180f;
        private float lastLaserFireTime;
        private float lastBulletFireTime;
        private bool canControl = true;
        [SerializeField] private float asteroidBounceForce = 5f;
        [SerializeField] private float controlLockDuration = 1f;

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
        private float playerBounceForce = 20f;

        private PlayerDataModel playerDataModel;

        [SerializeField] private int health;
        public int Health
        {
            get { return health; }
            private set { health = Mathf.Clamp(value, 0, MaxHealth); }
        }

        private DamageHandler damageHandler;

        [Inject]
        public void Construct(IControlStrategy controlStrategy, LaserManager laserManager,
            PlayerDataModel playerDataModel)
        {
            this.controlStrategy = controlStrategy;
            this.laserManager = laserManager;
            this.playerDataModel = playerDataModel;
            damageHandler = new DamageHandler(5, playerDataModel);
        }

       

        private void Awake()
        {
            movementController = new PlayerMovementController(transform, speed, acceleration, rotationSpeed);
            shootingController =
                new PlayerShootingController(laserPrefab, bulletPrefab, firePoint, laserCooldown, bulletCooldown);
            collisionHandler =
                new PlayerCollisionHandler(transform, health, MaxHealth, playerBounceForce, asteroidBounceForce,
                    invincibilityEffect, invincibilityDuration, damageHandler);
            collisionHandler.OnControlLockRequested += LockControlDuration;
            damageHandler.OnDeath += HandleDeath;
        }

        private void HandleDeath()
        {
            Debug.Log("Player is dead!");
            Destroy(gameObject);
        }

        private void Update()
        {
            if (canControl)
            {
                HandleInput();
                HandleMovement();
            }
            else
            {
                movementController.ApplyVelocity(); // Продолжаем движение по инерции
            }

            playerDataModel.Position.Value = transform.position;
            playerDataModel.Speed.Value = movementController.GetSpeed();
            playerDataModel.RotationAngle.Value = transform.eulerAngles.z;
           // playerDataModel.Health.Value = health ;
        }

        private void HandleInput()
        {
            if (controlStrategy.FireLaser() && laserManager.CanFireLaser())
            {
                shootingController.FireLaser();
                laserManager.UseLaser();
            }

            if (controlStrategy.FireBullet())
            {
                shootingController.FireBullet();
            }
        }


        public void HandleMovement()
        {
            Vector2 input = controlStrategy.GetInput();
            movementController.HandleMovement(input);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var (direction, force) = collisionHandler.CalculateBounce(other);
            movementController.AddVelocity(direction, force);
            
                collisionHandler.HandleCollision(other, controlLockDuration).Forget();
        }

        


        public void LockControlDuration(float duration)
        {
            LockControlForSeconds(duration);
        }

        private async UniTask LockControlForSeconds(float duration)
        {
            canControl = false;
            await UniTask.Delay((int)(duration * 1000));
            canControl = true;
        }
    }
}