using UnityEngine;

namespace Game.Entities.Asteroids {
    public enum AsteroidSize {
        Small,
        Medium,
        Large
    }

    public class Asteroid : MonoBehaviour, IDamageable {
        private Vector2 direction;
        [SerializeField] private AsteroidSize size;
        [SerializeField] private GameObject smallerAsteroidPrefab;
        [SerializeField] private int Health = 1;

        public void TakeDamage(int damage) {
            Health -= damage;

            if (Health >= 0) {
                BreakApart();
            }
            else {
                Destroy(gameObject);
            }
        }

        public void BreakApart() {
            if (size != AsteroidSize.Small) {
                SpawnSmallerAsteroids();
            }
            Destroy(gameObject);
        }

        private void SpawnSmallerAsteroids() {
            if (smallerAsteroidPrefab == null) {
                Debug.LogError("Prefab for smaller asteroids is not assigned!");
                return;
            }

            for (int i = 0; i < 2; i++) {
                Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
                GameObject smallerAsteroid = Instantiate(smallerAsteroidPrefab, spawnPosition, Quaternion.identity);

                AsteroidMovement asteroidScript = smallerAsteroid.GetComponent<AsteroidMovement>();
                asteroidScript.Speed += 3f;
            }
        }

        public void DestroyAsteroid() {
            Destroy(gameObject);
        }
    }
}