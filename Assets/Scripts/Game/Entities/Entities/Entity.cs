using UnityEngine;

namespace Game.Entities.Entities
{
    public class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int health = 1;

        public virtual void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                DestroyEntity();
            }
        }

        public virtual void DestroyEntity()
        {
            Destroy(gameObject);
        }
    }
}