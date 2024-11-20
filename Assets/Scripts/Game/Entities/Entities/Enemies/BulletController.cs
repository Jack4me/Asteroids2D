using UnityEngine;

namespace Game.Entities.Entities.Enemies {
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f; // Скорость полета пули
        [SerializeField] private float lifetime = 5f; // Время жизни пули

        private Vector2 direction; // Направление движения пули

        public void Initialize(Vector2 shootDirection) {
            direction = shootDirection.normalized;
            Destroy(gameObject, lifetime); // Уничтожаем пулю через заданное время
        }

        private void Update() {
            // Перемещаем пулю
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) {
            // Реакция на столкновение (например, уничтожение объекта)
            if (other.CompareTag("Player")) {
                Debug.Log("Bullet hit the player!");
                Destroy(gameObject);
            }
        }
    }
}
