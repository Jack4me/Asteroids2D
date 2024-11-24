using Game.Entities.Asteroids;
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

        // public override void DestroyEntity() {
        //     if (size != AsteroidSize.Small) {
        //         SpawnSmallerAsteroids();
        //     }
        //     base.DestroyEntity(); // Вызов базового метода для удаления объекта
        // }
        public override void TakeDamage(int damage)
        {
            if (size != AsteroidSize.Small)
            {
                SpawnSmallerAsteroids();
            }

            base.DestroyEntity();
        }

        private void SpawnSmallerAsteroids()
        {
            if (smallerAsteroidPrefab == null)
            {
                Debug.LogError("Prefab for smaller asteroids is not assigned!");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
                GameObject smallerAsteroid = Instantiate(smallerAsteroidPrefab, spawnPosition, Quaternion.identity);

                AsteroidMovement asteroidScript = smallerAsteroid.GetComponent<AsteroidMovement>();
                asteroidScript.Speed += 3f;
            }
        }
    }
}