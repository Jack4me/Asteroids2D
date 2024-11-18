using UnityEngine;

namespace Core {
    public interface IShipController
    {
        void Move(Vector2 direction);
        void Rotate(float angle);
        void Shoot();
    }
}

