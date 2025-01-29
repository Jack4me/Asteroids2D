using Core.Game.Entities.Hero;
using Core.Intrerfaces.Services.Input;
using Cysharp.Threading.Tasks;
using Game.Controllers;
using UnityEngine;

namespace Core.Game.Controllers
{
    public class HeroInput : MonoBehaviour
    {

        private IInputService _inputService;
        private bool canControl = true;
        private HeroCollisionHandler _heroCollisionHandler;
        public HeroMove heroMove { get; set; }

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _heroCollisionHandler = GetComponent<HeroCollisionHandler>();
            _heroCollisionHandler.OnControlLockRequested += LockControlDuration;
            heroMove = GetComponent<HeroMove>();
        }

        private void Update()
        {
            if (canControl)
            {
                HandleMovement(_inputService.Axis);
            }
            else
            {
                heroMove.Move();
            }

            heroMove.ApplyFriction();
        }

        public void Movable(Vector2 _inputService)
        {
            heroMove.Rotate(_inputService.x);
            heroMove.Accelerate(_inputService.y);
            heroMove.Move();
        }

        

        public void HandleMovement(Vector2 _inputService)
        {
            Movable(_inputService);
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