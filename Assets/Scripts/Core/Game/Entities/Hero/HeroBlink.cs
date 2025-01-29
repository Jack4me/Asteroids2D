using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Game.Entities.Hero
{
    public class HeroBlink : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRendererShip;
        [SerializeField] private SpriteRenderer spriteRendererShipWings;
        [SerializeField] private SpriteRenderer spriteRendererShipTail;
        [SerializeField] private float blinkDuration = 3f;
        [SerializeField] private float blinkInterval = 0.2f;
        [SerializeField] private float blinkFlaveInterval = 0.1f;
        private bool isBlinking = false;

        private void Start()
        {
           StartBlinkingForever();
        }

        public async UniTask StartBlinking()
        {
            float endTime = Time.time + blinkDuration;
            while (Time.time < endTime)
            {
                spriteRendererShip.enabled = !spriteRendererShip.enabled;
                spriteRendererShipWings.enabled = !spriteRendererShipWings.enabled;
                await UniTask.Delay(TimeSpan.FromSeconds(blinkInterval));
            }
            spriteRendererShip.enabled = true;
            spriteRendererShipWings.enabled = true;
        }
        
        public async UniTaskVoid StartBlinkingForever()
        {
            isBlinking = true;

            while (isBlinking)
            {
                spriteRendererShipTail.enabled = !spriteRendererShipTail.enabled;
                await UniTask.Delay(TimeSpan.FromSeconds(blinkFlaveInterval));
            }
            spriteRendererShipTail.enabled = true;
        }
    }
}