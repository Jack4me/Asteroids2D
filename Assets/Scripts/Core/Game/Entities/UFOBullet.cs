using System;
using Core.Game.Entities.Hero.Invincibility;
using Core.Game.Entities.Invincibility;
using Core.Game.Handlers.Health;
using Game.Controllers;
using UnityEngine;

namespace Core.Game.Entities
{
    public class UFOBullet : MonoBehaviour, IHit
    {
        [SerializeField] private int damage = 1;
        private readonly float speed = 15f;
        private readonly float lifeTime = 3f;
        private HeroCollisionHandler heroCollisionHandler;

        public int Damage { get; set; } = 1;

        private void Awake()
        {
            heroCollisionHandler = GetComponent<HeroCollisionHandler>();
        }

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        // private void OnTriggerEnter2D(Collider2D col)
        // {
        //     if (col.TryGetComponent(out HeroMove hero))
        //     {
        //         hero.GetComponent<HealthHandler>().TakeDamage(damage);
        //         heroCollisionHandler._invincibilityHandler.EnableInvincibility();
        //         Destroy(gameObject);
        //     }
        // }
    }
}