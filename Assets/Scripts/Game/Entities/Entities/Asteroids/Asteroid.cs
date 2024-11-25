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
        private ObjectPoolAstro _pool;
        public void Initialize(ObjectPoolAstro pool)
        {
            _pool = pool;
        }

        public override void TakeDamage(int damage)
        {
            if (size != AsteroidSize.Small)
            {
                SpawnSmallerAsteroids();
            }

            ReturnToPool();
        }

        public override void DestroyEntity()
        {
            ReturnToPool();
        }

        public override void ReturnToPool()
        {
            _pool.ReturnToPool(gameObject);
        }

        private void SpawnSmallerAsteroids()
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject smallerAsteroid = _pool.GetFromPool(smallerAsteroidPrefab);
                smallerAsteroid.transform.position = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;

                // var mover = smallerAsteroid.GetComponent<Mover>();
                // mover.Initialize(Random.insideUnitCircle.normalized, mover.Speed + 3f);
            }
        }
    }
    
}