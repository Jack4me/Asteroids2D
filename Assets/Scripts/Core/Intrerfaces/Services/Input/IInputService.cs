using Core.Services;
using UnityEngine;

namespace Core.Intrerfaces.Services.Input {
    public interface IInputService : IService {
        Vector2 Axis{ get; }

        bool IsAttackBulletButton();
        bool IsAttackLaserButton();
    }
}