using Game.Entities.Asteroids;
using UnityEngine;

namespace Game.Entities {
    public class PlayerBullet : MonoBehaviour {
        private readonly float speed = 15f;
        private readonly float lifeTime = 3f;

        private void Start() {
            Destroy(gameObject, lifeTime);
        }

        private void Update() {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<IDamageable>(out var asteroid)) {
                asteroid.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}