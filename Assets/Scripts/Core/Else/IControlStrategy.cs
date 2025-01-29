using UnityEngine;

namespace Core.Intrerfaces {
    public interface IControlStrategy {
        Vector2 GetInput();
        bool FireLaser();  
        bool FireBullet(); 
    }
}