using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Entities.Entities
{
    public class InvincibilityHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _invincibilityEffect;
        [SerializeField] private float _invincibilityDuration;
        public bool IsInvincible { get; private set; }
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
           IsInvincible = true;
           await UniTask.Delay((int)(_invincibilityDuration * 1000));
           HideInvincibilityEffect();
           IsInvincible = false;
           
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

       public void SetInvincibile(bool set)
       {
           IsInvincible = set;
       }
    }
}
