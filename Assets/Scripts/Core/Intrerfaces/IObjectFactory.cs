using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IObjectFactory
    {
        GameObject CreateAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent, IObjectPool pool);
        GameObject CreateMediumAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent, IObjectPool pool);
        GameObject CreateSmallAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent, IObjectPool pool);
        GameObject CreateUFO(Vector2 position, Vector2 direction, float speed, Transform poolParent, IObjectPool pool);
        
        GameObject GetAsteroidPrefab();
        GameObject GetMediumAsteroidPrefab();
        GameObject GetSmallAsteroidPrefab();
        GameObject GetUfoPrefab();
    }
}