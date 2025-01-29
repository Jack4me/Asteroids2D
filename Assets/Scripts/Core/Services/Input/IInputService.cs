using UnityEngine;

namespace Core.Intrerfaces.Services.Input {
    public interface IInputService
    {
        Vector2 Axis{ get; }

        bool IsAttackBulletButton();
        bool IsAttackLaserButton();
    }
}