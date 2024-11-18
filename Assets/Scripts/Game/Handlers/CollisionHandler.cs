using Game.Controllers;
using UnityEngine;

namespace Game.Handlers {
    public class CollisionHandler
    {
        // public void HandleCollision(ShipController ship, GameObject asteroid)
        // {
        //     // Логика столкновения и рикошета
        //     Vector2 shipVelocity = ship.GetVelocity(); // Получаем текущую скорость корабля
        //     Vector2 asteroidVelocity = asteroid.GetComponent<AsteroidController>().GetVelocity();
        //
        //     // Простейший рикошет: меняем направления вектора скоростей
        //     ship.SetVelocity(-shipVelocity);
        //     asteroid.GetComponent<AsteroidController>().SetVelocity(-asteroidVelocity);
        //
        //     // Понижаем здоровье или выполняем другие действия
        //     ship.TakeDamage();
        // }
    }
}