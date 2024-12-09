using Game.Controllers;
using UnityEngine;

namespace Game.Config
{
    public class PlayerStats : MonoBehaviour
    {
        public int health;
        public float speed;
        public string weaponName;
        private HeroMove _heroMove;
        private void Start()
        {
            FindFirstObjectByType<ConfigLoader>().
            _heroMove = GetComponent<HeroMove>();
            Debug.Log($"Игрок: здоровье={health}, скорость={speed}, оружие={weaponName}");
            _heroMove.SetSpeed(speed);
        }
    }
}
