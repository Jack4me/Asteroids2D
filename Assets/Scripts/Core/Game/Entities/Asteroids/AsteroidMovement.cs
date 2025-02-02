using Core.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Game.Entities.Asteroids
{
    public class AsteroidMovement : MonoBehaviour
    {
        private float _speed;
        private Vector2 direction;
        private Enemies _enemies;

        private void Start()
        {
            direction = Random.insideUnitCircle.normalized;
        }

        private void Update()
        {
            transform.position += (Vector3)(direction * _speed * Time.deltaTime);
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
    }
}