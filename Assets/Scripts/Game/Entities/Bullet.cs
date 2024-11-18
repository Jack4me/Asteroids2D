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
            if (other.TryGetComponent<IDamageable>(out var asteroid)) {
                asteroid.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}