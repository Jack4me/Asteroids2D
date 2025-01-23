using System;
using Core.Intrerfaces;
using Game.Controllers;
using Game.Handlers.Health;
using UnityEngine;

namespace Game.Config
{
    public class PlayerStats : MonoBehaviour, IPlayerStats
    {
        public int health { get; set; }
        public float speed { get; set; }
        public string weaponName { get; set; }

        private HeroInput _heroInput;
        private HealthHandler _healthHandler;

        private void Awake()
        {
            _heroInput = GetComponent<HeroInput>();
            _healthHandler = GetComponent<HealthHandler>();
            
        }

        private void Start()
        {
            if (_healthHandler == null)
            {
                Debug.LogError("HealthHandler is not assigned!");
                return;
            }
            Debug.Log($"Игрок: здоровье={health}, скорость={speed}, оружие={weaponName}");
            _healthHandler.SetStartHealth(health);
            _heroInput.SetSpeed(speed);
        }

        
    }
}
