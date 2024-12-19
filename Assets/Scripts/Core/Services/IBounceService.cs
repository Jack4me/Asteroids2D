using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Core.Services
{
    public interface IBounceService : IService
    {
        (Vector2 direction, float force) CalculateBounce(Vector2 objectPosition, Vector2 colliderPosition, float bounceForce);
        void ApplyBounce(Rigidbody2D rigidbody, Vector2 direction, float force);
    }
}