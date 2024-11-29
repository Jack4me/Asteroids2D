using UnityEngine;

namespace Game.Entities.Entities.Asteroids
{
    public class BounceController : MonoBehaviour
    {
        private Asteroid asteroid;
        private Vector2 velocity;

        private void Update()
        {
            HandleMovement();
        }

        public void ApplyBounce(Vector2 bounceForce)
        {
            velocity += bounceForce;
        }

        private void HandleMovement()
        {
            transform.position += (Vector3)velocity * Time.deltaTime;
            velocity *= 0.99f;
        }
    }
}