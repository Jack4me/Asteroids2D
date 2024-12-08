using CodeBase.Infrastructure.Services;
using Core.Intrerfaces.Services.Input;
using Cysharp.Threading.Tasks;
using Infrastructure.Ref.Services;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Game.Controllers
{
    public class HeroMove : MonoBehaviour
    {
        public float speed = 5f;
        private IInputService _inputService;
        private Camera _camera;
        private Vector2 velocity;
        private bool canControl = true;
        [SerializeField] private float rotationSpeed = 20f;
        [SerializeField] private float acceleration;
        private PlayerCollisionHandler playerCollision;

        public HeroMove Construct()
        {
            return this;
        }

        private void Awake()
        {
            _inputService = AllServices.Container.GetService<IInputService>();
            playerCollision = GetComponent<PlayerCollisionHandler>();

            playerCollision.OnControlLockRequested += LockControlDuration;
        }

        private void Update()
        {
            if (canControl)
            {
               
                    HandleMovement(_inputService.Axis);
                
            }

            else
            {
                ApplyVelocity();
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

        public void LockControlDuration(float duration)
        {
            LockControlForSeconds(duration);
        }

        private async UniTask LockControlForSeconds(float duration)
        {
            canControl = false;
            await UniTask.Delay((int)(duration * 1000));
            canControl = true;
        }
    }
}