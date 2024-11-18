using Game.Entities.Asteroids;
using UnityEngine;

namespace Game.Entities {
    public class Bullet : MonoBehaviour {
        public float Speed = 15f;
        public float LifeTime = 3f;

        private void Start() {
            Destroy(gameObject, LifeTime);
        }

        private void Update() {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            // Если столкнулись с астероидом
            if (other.TryGetComponent<Asteroid>(out var asteroid)) {
                asteroid.BreakApart(); // Разбиваем астероид
                Destroy(gameObject); // Уничтожаем пулю
            }

            // Если столкнулись с обломком или тарелкой (предполагается, что они имеют тег "Enemy")
            if (other.CompareTag("Enemy")) {
                Destroy(other.gameObject); // Уничтожаем объект
                Destroy(gameObject); // Уничтожаем пулю
            }
        }
    }
}