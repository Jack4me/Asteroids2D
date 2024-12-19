using UnityEngine;

namespace Core.Services
{
    public class BounceService : IBounceService
    {
        public (Vector2 direction, float force) CalculateBounce(Vector2 objectPosition, Vector2 colliderPosition, float bounceForce)
        {
            Vector2 collisionDirection = objectPosition - colliderPosition;
            collisionDirection.Normalize();
            return (collisionDirection, bounceForce);
        }

        public void ApplyBounce(Rigidbody2D rigidbody, Vector2 direction, float force)
        {
            rigidbody.velocity += direction * force;
        }
    }
}