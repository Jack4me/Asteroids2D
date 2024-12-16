using UnityEngine;

namespace Infrastructure.Config
{
    public class EnemyStats : MonoBehaviour
    {
        public int health;
        public float speed;
        public int damage;

        private void Start()
        {
            Debug.Log($"Враг: здоровье={health}, скорость={speed}, урон={damage}");
        }
    }
}
