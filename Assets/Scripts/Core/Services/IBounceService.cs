using UnityEngine;

namespace Core.Services
{
    public interface IBounceService
    {
        void ApplyBounce(Transform target, Collider2D collider, float bounceForce);
    }
}