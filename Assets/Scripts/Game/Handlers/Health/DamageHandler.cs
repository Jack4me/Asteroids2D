using System;
using Game.Models;
using UniRx;
using Zenject;

namespace Game.Handlers.Health
{
    public class DamageHandler
    {
        private readonly int maxHealth;
        private readonly int health;
        private readonly ReactiveProperty<int> currentHealth;
        private readonly PlayerDataModel playerDataModel;


        public event Action OnDeath; // Событие смерти

      
        public DamageHandler(int maxHealth, PlayerDataModel playerDataModel)
        {
            this.playerDataModel = playerDataModel;
        }

        public void TakeDamage(int damage)
        {
            playerDataModel.Health.Value = maxHealth;

            if (damage <= 0) return;

            playerDataModel.Health.Value -= damage;

            if (playerDataModel.Health.Value <= 0)
            {
                playerDataModel.Health.Value = 0;
                OnDeath?.Invoke();
            }
        }

        public void Heal(int amount)
        {
            if (amount <= 0) return;

            playerDataModel.Health.Value = Math.Min(playerDataModel.Health.Value + amount, maxHealth);
        }
        
    }
}