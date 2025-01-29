using Core.Else;
using Core.Game.Controllers;
using Core.Game.Handlers;
using UnityEngine;

namespace Core.Game.Entities.UFO
{
    public class EnemyBullet : MonoBehaviour, IHit
    {
        [SerializeField] private int damage = 1;
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
            if (col.TryGetComponent(out HeroInput hero))
            {
                hero.GetComponent<HealthHandler>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}