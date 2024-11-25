using Infrastructure;
using UnityEngine;

namespace Game.Entities.Entities.Asteroids
{
    public enum AsteroidSize
    {
        Small,
        Medium,
        Large
    }

    public class Asteroid : Entity
    {
        private Vector2 direction;
        [SerializeField] private AsteroidSize size;
        [SerializeField] private GameObject smallerAsteroidPrefab;


        public override void TakeDamage(int damage)
        {
            if (size != AsteroidSize.Small)
            {
                SpawnSmallerAsteroids();
            }

            base.TakeDamage(damage); // Вызываем базовую логику получения урона
        }

        private void SpawnSmallerAsteroids()
        {
            for (int i = 0; i < 2; i++)
            {
                var smallerAsteroid = _pool.GetFromPool(smallerAsteroidPrefab); // Берём объект из пула
                smallerAsteroid.transform.position = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;

                // var mover = smallerAsteroid.GetComponent<Mover>();
                // mover.Initialize(Random.insideUnitCircle.normalized, mover.Speed + 3f);
            }
        }
    }
}