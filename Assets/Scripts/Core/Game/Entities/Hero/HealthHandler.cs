using Core.Intrerfaces;
using UniRx;
using UnityEngine;

namespace Core.Game.Handlers
{
    public class HealthHandler : MonoBehaviour
    {
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
            }
        }

        private void DeathPlayer()
        {
            Destroy(gameObject);
        }
    }
}