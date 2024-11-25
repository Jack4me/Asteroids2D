using System;
using UnityEngine;

namespace Game.Entities.Entities.Enemies
{
    public class EnemyBullet : MonoBehaviour
    {
        private readonly float speed = 15f;
        private readonly float lifeTime = 3f;

        private void Start() {
            Destroy(gameObject, lifeTime);
        }

        private void Update() {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }
}
