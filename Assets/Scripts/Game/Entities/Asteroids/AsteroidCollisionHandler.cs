using UnityEngine;

namespace Game.Entities.Asteroids {
    public class AsteroidCollisionHandler : MonoBehaviour {
        private Asteroid asteroid;

        private void Start() {
            asteroid = GetComponent<Asteroid>();
        }

        private void Update() {
            // Проверяем столкновения с лазерами
            GameObject[] lasers = GameObject.FindGameObjectsWithTag("Laser");
            foreach (var laser in lasers) {
                if (Vector2.Distance(transform.position, laser.transform.position) < 1f) {
                    OnLaserHit(laser);
                }
            }
        }

        private void OnLaserHit(GameObject laser) {
            asteroid.BreakApart(); 
        }
    }
}