using CodeBase.Infrastructure.Services;
using Core.Intrerfaces.Services.Input;
using Cysharp.Threading.Tasks;
using Infrastructure.Ref.Services;
using UnityEngine;

namespace Game.Controllers
{
    public class HeroMove : MonoBehaviour
    {
        private float _speed = 5f;
        private IInputService _inputService;
        public Vector2 velocity;
        private bool canControl = true;
        [SerializeField] private float rotationSpeed = 20f;
        [SerializeField] private float acceleration;
        private PlayerCollisionHandler playerCollision;

        public float CurrentSpeed;

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
                Move();
            }

            ApplyFriction();
            CurrentSpeed = velocity.magnitude;
        }

        public void HandleMovement(Vector2 _inputService)
        {
            Rotate(_inputService.x);
            Accelerate(_inputService.y);
            Move();
        }

        private void ApplyFriction()
        {
            velocity *= 0.9999f;
            if (velocity.magnitude < 0.01f)
            {
                velocity = Vector2.zero;
            }
        }

        private void Rotate(float rotationInput)
        {
            float rotation = -rotationInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotation);
        }


        

        private void Accelerate(float accelerationInput)
        {
            velocity += (Vector2)transform.up * accelerationInput * acceleration;

            if (velocity.magnitude > _speed)
            {
                velocity = velocity.normalized * _speed;
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

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
    }
}