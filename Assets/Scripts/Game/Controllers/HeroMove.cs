using UnityEngine;

namespace Game.Controllers
{
    public class HeroMove : MonoBehaviour
    {
        public Vector2 velocity;
        private float _speed = 5f;
        [SerializeField] private float rotationSpeed = 20f;
        [SerializeField] private float acceleration;

        

        public void Accelerate(float accelerationInput)
        {
            velocity += (Vector2)transform.up * accelerationInput * acceleration;
            if (velocity.magnitude > _speed)
            {
                velocity = velocity.normalized * _speed;
            }
        }

        public void Rotate(float rotationInput)
        {
            float rotation = -rotationInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotation);
        }

        public void Move()
        {
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
        
        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void ApplyFriction()
        {
            velocity *= 0.9990f;
            if (velocity.magnitude < 0.01f)
            {
                velocity = Vector2.zero;
            }
        }
    }
}