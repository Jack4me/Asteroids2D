using UnityEngine;

namespace Game.Entities.Entities.Asteroids {
    public class AsteroidMovement : MonoBehaviour {
        public float Speed = 2f;
        private Vector2 direction;

        private void Start() {
            direction = Random.insideUnitCircle.normalized; 
        }

        private void Update() {
            transform.position += (Vector3)(direction * Speed * Time.deltaTime);
        }

        
    }
}