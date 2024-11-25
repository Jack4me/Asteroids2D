using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IObjectFactory
    {
        GameObject CreateAsteroid(Vector2 position, Vector2 direction, float speed);
        GameObject CreateUFO(Vector2 position, Vector2 direction, float speed);
    }
}