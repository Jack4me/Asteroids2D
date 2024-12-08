using Core;
using Core.Intrerfaces;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Entities.Entities.Asteroids
{
    

    public class Asteroid : Enemy
    {
        private Vector2 direction;
        [SerializeField] private EnemyType size;
        [SerializeField] private GameObject smallerAsteroidPrefab;
        public Asteroid(IObjectPool objectPool) : base(objectPool)
        {
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