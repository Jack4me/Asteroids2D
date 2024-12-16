using UnityEngine;

namespace Game.Controllers
{
    public class PlayerMovementController
    {
        private Transform playerTransform;
        private float acceleration;
        public float speed { get; private set; }
        private float rotationSpeed;
        private Vector2 velocity;

        public PlayerMovementController(Transform playerTransform, float speed, float acceleration, float rotationSpeed)
        {
            this.playerTransform = playerTransform;
            this.speed = speed;
            this.acceleration = acceleration;
            this.rotationSpeed = rotationSpeed;
            velocity = Vector2.zero;
        }

        public void HandleMovement(Vector2 input)
        {
            Rotate(input.x);
            Accelerate(input.y);
            Move();
        }

        private void Rotate(float rotationInput)
        {
            float rotation = -rotationInput * rotationSpeed * Time.deltaTime;
            playerTransform.Rotate(0f, 0f, rotation);
        }

        private void Accelerate(float accelerationInput)
        {
            velocity += (Vector2)playerTransform.up * accelerationInput * acceleration;

            if (velocity.magnitude > speed)
            {
                velocity = velocity.normalized * speed;
            }
        }

        private void Move()
        {
            playerTransform.position += (Vector3)velocity * Time.deltaTime;
        }

        public float GetSpeed()
        {
            return velocity.magnitude;
        }

        public void ApplyVelocity()
        {
            playerTransform.position += (Vector3)velocity * Time.deltaTime;
        }

        public void AddVelocity(Vector2 direction, float force)
        {
            velocity += direction * force;
        }

        public void SetVelocity(Vector2 newVelocity)
        {
            velocity = newVelocity;
        }

        public Vector2 GetVelocity()
        {
            return velocity;
        }
    }
}