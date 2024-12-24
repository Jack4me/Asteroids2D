using UnityEngine;

namespace Core.Services
{
    public interface IBounceService : IService
    {
        (Vector2 direction, float force) CalculateBounce(Vector2 objectPosition, Vector2 colliderPosition, float bounceForce);
        void ApplyBounce(Transform target, Collider2D collider, float bounceForce);
    }
}