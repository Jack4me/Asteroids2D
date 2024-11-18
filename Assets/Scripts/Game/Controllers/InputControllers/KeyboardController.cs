using Core.Intrerfaces;
using UnityEngine;

namespace Game.InputControllers {
    public class KeyboardController : IControlStrategy
    {
       
        public Vector2 GetInput()
        {
            float horizontal = Input.GetAxis("Horizontal"); 
            float vertical = Input.GetAxis("Vertical"); 
            return new Vector2(horizontal, vertical);
        }
        public bool FireLaser() {
            return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1) ;
        }

        public bool FireBullet() {
            return Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Mouse0);
        }
    }

}