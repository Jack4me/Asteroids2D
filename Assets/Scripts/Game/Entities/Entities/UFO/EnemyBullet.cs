using System;
using Core;
using UnityEngine;

namespace Game.Entities.Entities.Enemies
{
    public class EnemyBullet : MonoBehaviour, IHit
    {
        private readonly float speed = 15f;
        private readonly float lifeTime = 3f;
        [field: SerializeField]  public int Damage { get; set; }

        private void Start() {
            Destroy(gameObject, lifeTime);
        }

        private void Update() {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

    }
}
