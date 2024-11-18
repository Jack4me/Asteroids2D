using UnityEngine;

namespace Game.Entities.Asteroids {
    public class Asteroid : MonoBehaviour {
        public float Speed = 2f;
        public int Size = 3; // Размер астероида (3 — крупный, 2 — средний, 1 — мелкий) 
        private Vector2 direction;
        [SerializeField] private GameObject smallerAsteroidPrefab;

 
        
        public void BreakApart() {
            if (Size > 1) {
                for (int i = 0; i < 2; i++) {
                    GameObject smallerAsteroid = Instantiate(smallerAsteroidPrefab, transform.position, Quaternion.identity);
                    AsteroidMovement asteroidScript = smallerAsteroid.GetComponent<AsteroidMovement>();
                  
                    asteroidScript.Speed += 5f; // Увеличиваем скорость
                }
            }

            Destroy(gameObject); // Уничтожаем текущий астероид
        }
    }
}