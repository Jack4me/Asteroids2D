using UnityEngine;

namespace Game.Entities.Entities.Enemies {
    public class ShootingEnemyController : MonoBehaviour {
        [Header("Movement Settings")] [SerializeField]
        private float speed = 3f; // Скорость передвижения врага

        [SerializeField] private float stopDistance = 1.5f; // Расстояние, на котором враг останавливается от игрока

        [Header("Shooting Settings")] [SerializeField]
        private GameObject bulletPrefab; // Префаб пули

        [SerializeField] private Transform firePoint; // Точка выстрела
        [SerializeField] private float shootCooldown = 5f; // Время между выстрелами

        private Transform playerTransform; // Ссылка на игрока
        private Vector2 velocity; // Скорость для кастомной физики
        private float lastShootTime; // Время последнего выстрела

        private void Start() {
            // Находим игрока по тегу
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) {
                playerTransform = player.transform;
            }
            else {
                Debug.LogError("Player not found in the scene!");
            }
        }

        private void Update() {
            if (playerTransform != null) {
                HandleMovement();
                HandleShooting();
            }
        }

        private void HandleMovement() {
            // Направление к игроку
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Расстояние до игрока
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // Если враг находится дальше, чем расстояние остановки, он продолжает двигаться
            if (distanceToPlayer > stopDistance) {
                velocity = directionToPlayer * speed;
                transform.position += (Vector3)velocity * Time.deltaTime;

                // Поворот врага в сторону игрока
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            else {
                // Если враг близко к игроку, он останавливается
                velocity = Vector2.zero;
            }
        }

        private void HandleShooting() {
            // Если прошло достаточно времени с момента последнего выстрела
            if (Time.time - lastShootTime >= shootCooldown) {
                ShootAtPlayer();
                lastShootTime = Time.time;
            }
        }

        private void ShootAtPlayer() {
            if (bulletPrefab != null && firePoint != null && playerTransform != null) {
                // Создаем пулю
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                // Направление выстрела в сторону игрока
                Vector2 shootDirection = (playerTransform.position - firePoint.position).normalized;

                // Устанавливаем направление пули
                bullet.GetComponent<BulletController>()?.Initialize(shootDirection);

                Debug.Log("Enemy fired at player!");
            }
        }
    }
}
