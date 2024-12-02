using UnityEngine;

namespace Game.Entities.Entities.Enemies
{
    public class ShootingEnemyController : MonoBehaviour
    {
        [Header("Movement Settings")] [SerializeField]
        private float speed = 3f; 
        [SerializeField] private float stopDistance = 1.5f; 
        [Header("Shooting Settings")] [SerializeField]
        private GameObject bulletPrefab; 

        [SerializeField] private Transform firePoint; 
        [SerializeField] private float shootCooldown = 5f; 

        private Transform playerTransform; 
        private Vector2 velocity; 
        private float lastShootTime; 

        private void Start()
        {
            // 
            // ПРОКИНУТЬ ИГРОКА ЧЕРЕЗ ЗЕНЖЕКТ
            // 
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("Player not found in the scene!");
            }
        }

        private void Update()
        {
            if (playerTransform != null)
            {
                HandleMovement();
                HandleShooting();
            }
        }

        private void HandleMovement()
        {
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            // Если враг находится дальше, чем расстояние остановки, он продолжает двигаться
            if (distanceToPlayer > stopDistance)
            {
                velocity = directionToPlayer * speed;
                transform.position += (Vector3)velocity * Time.deltaTime;
                // Поворот врага в сторону игрока
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            else
            {
                // Если враг близко к игроку, он останавливается
                velocity = Vector2.zero;
            }
        }

        private void HandleShooting()
        {
            // Если прошло достаточно времени с момента последнего выстрела
            if (Time.time - lastShootTime >= shootCooldown)
            {
                ShootAtPlayer();
                lastShootTime = Time.time;
            }
        }

        private void ShootAtPlayer()
        {
            if (bulletPrefab != null && firePoint != null && playerTransform != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Vector2 shootDirection = (playerTransform.position - firePoint.position).normalized;
                bullet.GetComponent<BulletController>()?.Initialize(shootDirection);
                Debug.Log("Enemy fired at player!");
            }
        }
    }
}