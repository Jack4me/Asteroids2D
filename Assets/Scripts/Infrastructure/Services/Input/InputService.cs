using Services.Input;
using UnityEngine;

namespace Infrastructure.Services.Input {
    public abstract class InputService : IInputService {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fire1";
        public abstract Vector2 Axis{ get; }

        public bool IsAttackButtonUp(){
            return SimpleInput.GetButtonUp(Button);
        }

        protected static Vector2 SimpleInputAxis(){
            return new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}