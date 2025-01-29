using Core.Intrerfaces.Services.Input;
using UnityEngine;

namespace Core.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fire1";
        private const string Button2 = "Fire2";
        public abstract Vector2 Axis { get; }

        public bool IsAttackBulletButton()
        {
            return SimpleInput.GetButtonUp(Button);
        }

        public bool IsAttackLaserButton()
        {
            return SimpleInput.GetButtonUp(Button2);
        }

        protected static Vector2 SimpleInputAxis()
        {
            return new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}