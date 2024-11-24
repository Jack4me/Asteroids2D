using Game.Entities.Asteroids;
using UnityEngine;

namespace Game.Entities
{
    public class Laser : MonoBehaviour
    {
        public float Speed = 20f;
        public float LifeTime = 3f;
        [SerializeField] private int dmg = 3;

        private void Start()
        {
            Destroy(gameObject, LifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var asteroid))
            {
                asteroid.DestroyEntity();
            }
        }
    }
}