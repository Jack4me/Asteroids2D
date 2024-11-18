using Core.Intrerfaces;
using UnityEngine;

namespace Game.InputControllers {
    public class MobileController : IControlStrategy
    {
        public Vector2 GetInput()
        {
            float horizontal = UnityEngine.Input.acceleration.x; 
            float vertical = UnityEngine.Input.acceleration.y; 
            return new Vector2(horizontal, vertical);
        }

        public bool FireLaser() {
            throw new System.NotImplementedException();
        }

        public bool FireBullet() {
            throw new System.NotImplementedException();
        }
    }

}