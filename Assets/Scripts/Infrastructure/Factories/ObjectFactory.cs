using System;
using Core;
using Core.Intrerfaces;
using Game.Entities.Entities;
using UnityEngine;
using IObjectFactory = Core.Intrerfaces.IObjectFactory;
using Object = UnityEngine.Object;

namespace Infrastructure.Factories
{
    public class ObjectFactory : IObjectFactory
    {
        private GameObject asteroidPrefab;
        private GameObject mediumAsteroid;
        private GameObject smallAsteroid;

        private GameObject ufoPrefab;

        public ObjectFactory(GameObject asteroidPrefab, GameObject ufoPrefab, GameObject mediumAsteroid, GameObject smallAsteroid )
        {
            this.asteroidPrefab = asteroidPrefab;
            this.mediumAsteroid = mediumAsteroid;
            this.smallAsteroid = smallAsteroid;
            this.ufoPrefab = ufoPrefab;
        }

        public GameObject CreateAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent,
            IObjectPool pool, ScoreManager scoreManager)
        {
            var asteroid = Object.Instantiate(asteroidPrefab, position, Quaternion.identity, parent);
            var entity = asteroid.GetComponent<Enemy>();
            entity.scoreManager = scoreManager;
            if (entity != null)
            {
                entity._pool = pool; // Передаём пул в сущность
            }
            return asteroid;
        }

        public GameObject CreateMediumAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent,
            IObjectPool pool, ScoreManager scoreManager)
        {
            var asteroid = Object.Instantiate(mediumAsteroid, position, Quaternion.identity, parent);
            var entity = asteroid.GetComponent<Enemy>();
            entity.scoreManager = scoreManager;
            if (entity != null)
            {
                entity._pool = pool; // Передаём пул в сущность
            }
            return asteroid;
        }

        public GameObject CreateSmallAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent,
            IObjectPool pool, ScoreManager scoreManager)
        {
            var asteroid = Object.Instantiate(smallAsteroid, position, Quaternion.identity, parent);
            var entity = asteroid.GetComponent<Enemy>();
            entity.scoreManager = scoreManager;
            if (entity != null)
            {
                entity._pool = pool; // Передаём пул в сущность
            }
            return asteroid;
        }

        public GameObject CreateUFO(Vector2 position, Vector2 direction, float speed, Transform parent,
            IObjectPool pool, ScoreManager scoreManager)
        {
            var ufo = Object.Instantiate(ufoPrefab, position, Quaternion.identity, parent);
            var entity = ufo.GetComponent<Enemy>();
            entity.scoreManager = scoreManager;
            if (entity != null)
            {
                entity._pool = pool; // Передаём пул в сущность
            }
            return ufo;
        }


        public GameObject GetAsteroidPrefab()
        {
            return asteroidPrefab;
        }

        public GameObject GetMediumAsteroidPrefab()
        {
            return mediumAsteroid;
        }

        public GameObject GetSmallAsteroidPrefab()
        {
            return smallAsteroid;
        }

        public GameObject GetUfoPrefab()
        {
            return ufoPrefab;
        }
    }
}