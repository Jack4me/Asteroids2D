using UnityEngine;

namespace Game {
    public class Enemy : MonoBehaviour, IDamageable {
        [SerializeField] private int health = 3;

        public void TakeDamage(int amount) {
            health -= amount;

            if (health <= 0) {
                Destroy(gameObject); // Уничтожаем врага, если здоровье <= 0
            }
        }
    }
}