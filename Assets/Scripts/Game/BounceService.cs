using Core.Services;
using Cysharp.Threading.Tasks;
using Game.Controllers;
using UnityEngine;

namespace Game
{
    public class BounceService : IBounceService
    {
        public (Vector2 direction, float force) CalculateBounce(Vector2 objectPosition, Vector2 colliderPosition, float bounceForce)
        {
            throw new System.NotImplementedException();
        }

        public async void ApplyBounce(Transform target, Collider2D collider, float bounceForce)
        {
            Vector2 collisionDirection = (Vector2)(target.position - collider.transform.position).normalized;
            Vector2 startPosition = target.position;
            Vector2 targetPosition = startPosition + collisionDirection * bounceForce;

            float duration = 1f; 
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / duration;

               
                target.position = Vector2.Lerp(startPosition, targetPosition, progress);

                await UniTask.Yield();
            }

        }
    }
}