using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Game.Entities.Asteroids
{
    public class Asteroid : Enemy
    {
        [SerializeField] private EnemyType size;
        [SerializeField] private GameObject smallerAsteroidPrefab;
        private Vector2 direction;
        private AsteroidMovement asteroid;

        private void Awake()
        {
            asteroid = GetComponent<AsteroidMovement>();
        }

        private void Start()
        {
            asteroid.SetSpeed(Speed);
        }

        public override void TakeDamage(int damage)
        {
            if (size != EnemyType.Small)
            {
                SpawnSmallerAsteroids();
                ReturnToPool();
            }
            else
            {
                ReturnToPool();
            }

            base.TakeDamage(damage);
        }

        private void SpawnSmallerAsteroids()
        {
            if (PoolAsteroidGame == null)
            {
                Debug.LogError("_pool is not assigned!", this);
                return;
            }

            var nextType = GetNextAsteroidType(size);
            if (nextType == EnemyType.None)
            {
                Debug.Log("No smaller asteroid type available.");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                if (smallerAsteroidPrefab == null) return;
                var smallerAsteroid = PoolAsteroidGame.GetFromPool(nextType);
                smallerAsteroid.SetActive(true);
                smallerAsteroid.transform.position = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
            }
        }
    }
}