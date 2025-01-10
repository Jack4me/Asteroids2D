using Core.Intrerfaces.Services.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Controllers
{
    public class HeroMove : MonoBehaviour
    {
        public Vector2 velocity;
        private float _speed = 5f;
        private IInputService _inputService;
        private bool canControl = true;
        [SerializeField] private float rotationSpeed = 20f;
        [SerializeField] private float acceleration;
        private PlayerCollisionHandler playerCollision;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
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
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void HandleMovement(Vector2 _inputService)
        {
            Rotate(_inputService.x);
            Accelerate(_inputService.y);
            Move();
        }

        public void LockControlDuration(float duration)
        {
            LockControlForSeconds(duration);
        }

        private void ApplyFriction()
        {
            velocity *= 0.9990f;
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

        private async UniTask LockControlForSeconds(float duration)
        {
            canControl = false;
            await UniTask.Delay((int)(duration * 1000));
            canControl = true;
        }
    }
}