using System;
using UnityEngine;
using IObjectFactory = Core.Intrerfaces.IObjectFactory;
using Object = UnityEngine.Object;

namespace Infrastructure.Factories
{
    public class ObjectFactory : IObjectFactory
    {
        private GameObject asteroidPrefab;
        private GameObject mediumAsteroid;

        private GameObject ufoPrefab;

        public ObjectFactory(GameObject asteroidPrefab, GameObject ufoPrefab, GameObject mediumAsteroid)
        {
            this.asteroidPrefab = asteroidPrefab;
            this.ufoPrefab = ufoPrefab;
            this.mediumAsteroid = mediumAsteroid;
        }

        public GameObject CreateAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent)
        {
            var asteroid = Object.Instantiate(asteroidPrefab, position, Quaternion.identity, parent);

            return asteroid;
        }
        public GameObject CreateMediumAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent)
        {
            var asteroid = Object.Instantiate(mediumAsteroid, position, Quaternion.identity, parent);

            return asteroid;
        }

        public GameObject CreateUFO(Vector2 position, Vector2 direction, float speed, Transform poolParent)
        {
            var ufo = Object.Instantiate(ufoPrefab, position, Quaternion.identity, poolParent);

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

        public GameObject GetUfoPrefab()
        {
            return ufoPrefab;
        }
    }
}