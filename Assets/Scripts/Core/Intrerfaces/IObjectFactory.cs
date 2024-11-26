using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IObjectFactory
    {
        GameObject CreateAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent);
        GameObject CreateMediumAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent);
        GameObject CreateSmallAsteroid(Vector2 position, Vector2 direction, float speed, Transform parent);
        GameObject CreateUFO(Vector2 position, Vector2 direction, float speed, Transform poolParent);
        
        GameObject GetAsteroidPrefab();
        GameObject GetMediumAsteroidPrefab();
        GameObject GetSmallAsteroidPrefab();
        GameObject GetUfoPrefab();
    }
}