using Core;
using Game.Controllers;
using UnityEngine;

namespace Game.Entities.Entities.UFO
{
    public class EnemyBullet : MonoBehaviour, IHit
    {
        private readonly float speed = 15f;
        private readonly float lifeTime = 3f;
         public int Damage { get; set; }

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out HeroMove heroMove))
            {
                Destroy(gameObject);
            }
        }
    }
}