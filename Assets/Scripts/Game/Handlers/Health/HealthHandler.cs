using System;
using Core;
using Core.Ads_Plugin;
using Core.Intrerfaces;
using Infrastructure.Ref.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Handlers.Health
{
    public class HealthHandler : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        private readonly int health;
        private readonly ReactiveProperty<int> currentHealth;
        private IPlayerDataModel playerDataModel;


        public event Action OnDeath; // Событие смерти


        private void Awake()
        {
            playerDataModel = AllServices.Container.GetService<IPlayerDataModel>();
        }

        public void TakeDamage(int damage)
        {
            playerDataModel.Health.Value = Mathf.Max(0, playerDataModel.Health.Value - damage);

            if (playerDataModel.Health.Value <= 0)
            {
                DeathPlayer();
                OnDeath?.Invoke();
            }
        }

        private void DeathPlayer()
        {
            AdsManager.Instance.interstitialAds.ShowInterstitialAd();

            Destroy(gameObject);
        }

        public void Heal(int amount)
        {
            if (amount <= 0) return;

            playerDataModel.Health.Value = Math.Min(playerDataModel.Health.Value + amount, maxHealth);
        }

        public void SetStartHealth(int hp)
        {
            maxHealth = hp;
            playerDataModel.Health.Value = maxHealth;
        }
    }
}