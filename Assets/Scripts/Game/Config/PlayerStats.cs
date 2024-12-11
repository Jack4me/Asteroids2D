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

        private HeroMove _heroMove;
        private HealthHandler _healthHandler;

        private void Awake()
        {
            _heroMove = GetComponent<HeroMove>();
            _healthHandler = GetComponent<HealthHandler>();
            
        }

        private void Start()
        {
            
            Debug.Log($"Игрок: здоровье={health}, скорость={speed}, оружие={weaponName}");
            _healthHandler.SetStartHealth(health);
            _heroMove.SetSpeed(speed);
        }

        
    }
}
