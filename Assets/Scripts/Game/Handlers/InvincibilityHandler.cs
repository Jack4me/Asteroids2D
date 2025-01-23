using System;
using Cysharp.Threading.Tasks;
using Game.Hero;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.Handlers
{
    public class InvincibilityHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _invincibilityEffect;
        [SerializeField] private float _invincibilityDuration;
        public bool _isInvincible { get; private set; }
        private Collider2D _playerCollider;

        private void Awake()
        {
            HideInvincibilityEffect();
            _playerCollider = GetComponent<Collider2D>();

        }

        public async UniTask EnableInvincibility()
       {
           ShowInvincibilityEffect();
           await GetComponent<HeroBlink>().StartBlinking();
           _isInvincible = true;
           await UniTask.Delay((int)(_invincibilityDuration * 1000));
           HideInvincibilityEffect();
           _isInvincible = false;
           
           _playerCollider.enabled = true;
       }

       private void ShowInvincibilityEffect()
       {
           if (_invincibilityEffect != null)
           {
               _invincibilityEffect.gameObject.SetActive(true);
               _invincibilityEffect.Play();
           }
       }

       public void HideInvincibilityEffect()
       {
           _invincibilityEffect.Stop();
           _invincibilityEffect.gameObject.SetActive(false);
       }

       public void SetInvincibileTrue()
       {
           _isInvincible = true;
       }
    }
}
