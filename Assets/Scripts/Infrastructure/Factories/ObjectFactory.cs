using System;
using UnityEngine;
using IObjectFactory = Core.Intrerfaces.IObjectFactory;
using Object = UnityEngine.Object;

namespace Infrastructure.Factories
{
    public class ObjectFactory : IObjectFactory
    {
        private GameObject _asteroidPrefab;
        private GameObject _ufoPrefab;

        public ObjectFactory(GameObject asteroidPrefab, GameObject ufoPrefab)
        {
            _asteroidPrefab = asteroidPrefab;
            _ufoPrefab = ufoPrefab;
        }

        public GameObject CreateAsteroid(Vector2 position, Vector2 direction, float speed)
        {
            var asteroid = Object.Instantiate(_asteroidPrefab, position, Quaternion.identity);
         
            return asteroid;
        }

        public GameObject CreateUFO(Vector2 position, Vector2 direction, float speed)
        {
            var ufo = Object.Instantiate(_ufoPrefab, position, Quaternion.identity);
          
            return ufo;

        }

        public object Create(Type type)
        {
            throw new NotImplementedException();
        }
    }
}