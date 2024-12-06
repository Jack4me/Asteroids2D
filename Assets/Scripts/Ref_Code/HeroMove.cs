using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace Ref_Code
{
    public class HeroMove : MonoBehaviour
    {
        public float speed = 4.0f;
        private IInputService _inputService;
        private Camera _camera;
        private Vector2 velocity;
        [SerializeField] private float rotationSpeed = 20f;
        [SerializeField] private float acceleration;

        public HeroMove Construct(){
            return this;
        }

        private void Awake(){
            _inputService = AllServices.Container.GetService<IInputService>();
        }
        private void Update(){
            if (_inputService.Axis.sqrMagnitude > 0.01f){
                HandleMovement(_inputService.Axis);
            }
            else
            {
                transform.position += (Vector3)velocity  *Time.deltaTime;
            }
           
        }
        public void HandleMovement(Vector2 _inputService)
        {
            Rotate(_inputService.x);
            Accelerate(_inputService.y);
            Move();
        }

        private void Rotate(float rotationInput)
        {
            float rotation = -rotationInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotation);
        }
        public void ApplyVelocity()
        {
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
        public void AddVelocity(Vector2 direction, float force)
        {
            velocity += direction * force;
        }
        private void Accelerate(float accelerationInput)
        {
            velocity += (Vector2)transform.up * accelerationInput * acceleration;

            if (velocity.magnitude > speed)
            {
                velocity = velocity.normalized * speed;
            }
        }

        private void Move()
        {
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
       
    }
}
