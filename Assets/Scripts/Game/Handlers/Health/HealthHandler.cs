using System;
using Core.Ads_Plugin;
using Core.Intrerfaces;
using UniRx;
using UnityEngine;

namespace Game.Handlers.Health
{
    public class HealthHandler : MonoBehaviour
    {
        public event Action OnDeath;
        [SerializeField] private int maxHealth;
        private readonly int health;
        private readonly ReactiveProperty<int> currentHealth;
        private IPlayerDataModel _playerDataModel;

        public void Construct(IPlayerDataModel playerDataModel)
        {
            _playerDataModel = playerDataModel;
            if (_playerDataModel == null)
            {
                Debug.LogError("_playerDataModel");
            }
        }

        public void SetStartHealth(int hp)
        {
            maxHealth = hp;
            _playerDataModel.Health.Value = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _playerDataModel.Health.Value = Mathf.Max(0, _playerDataModel.Health.Value - damage);
            if (_playerDataModel.Health.Value <= 0)
            {
                DeathPlayer();
                OnDeath?.Invoke();
            }
        }

        private void DeathPlayer()
        {
           // AdsService.Instance.interstitialAds.ShowInterstitialAd();
            Destroy(gameObject);
        }
    }
}