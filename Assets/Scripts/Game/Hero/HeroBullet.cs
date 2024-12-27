using Core.Intrerfaces;
using UnityEngine;

namespace Game.Hero {
    public class HeroBullet : MonoBehaviour {
        private readonly float speed = 15f;
        private readonly float lifeTime = 3f;

        private void Start() {
            Destroy(gameObject, lifeTime);
        }

        private void Update() {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(1);
                Destroy(gameObject); 
            }
        }
    }
}