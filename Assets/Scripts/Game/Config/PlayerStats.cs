using Core.Intrerfaces;
using Game.Controllers;
using UnityEngine;

namespace Game.Config
{
    public class PlayerStats : MonoBehaviour, IPlayerStats
    {
        public int health { get; set; }
        public float speed { get; set; }
        public string weaponName { get; set; }

        private HeroMove _heroMove;
        private IConfigLoader _configLoader;

        public void Constarct(IConfigLoader configLoader)
        {
            _configLoader = configLoader;
        }
        private void Start()
        {
            
            _heroMove = GetComponent<HeroMove>();
            Debug.Log($"Игрок: здоровье={health}, скорость={speed}, оружие={weaponName}");
            
            _heroMove.SetSpeed(speed);
        }

        
    }
}
