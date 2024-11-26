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
                ReturnToPool();
                Debug.Log("1");
            }
            else
            {
                Debug.Log("2");

                ReturnToPool();
            }

          base.TakeDamage(damage); // Вызываем базовую логику получения урона
        }

        private void SpawnSmallerAsteroids()
        {
            if (_pool == null)
            {
                Debug.LogError("_pool is not assigned!", this);
                return; // Прерывает выполнение метода, если префаб не назначен
            }
            for (int i = 0; i < 2; i++)
            {
                if (smallerAsteroidPrefab == null) return;
                var smallerAsteroid = _pool.GetFromPool(smallerAsteroidPrefab);
                
                smallerAsteroid.SetActive(true);
                smallerAsteroid.transform.position = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
            }
        }
    }
}