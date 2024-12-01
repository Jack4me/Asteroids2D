using UnityEngine;

public class EnemyBoundaryHandler : MonoBehaviour {
    private const float BoundarySizeX = 17f;
    private const float BoundarySizeY = 11f;

    private void LateUpdate() {
        Vector3 position = transform.position;

        if (position.x > BoundarySizeX) position.x = -BoundarySizeX;
        else if (position.x < -BoundarySizeX) position.x = BoundarySizeX;

        if (position.y > BoundarySizeY) position.y = -BoundarySizeY;
        else if (position.y < -BoundarySizeY) position.y = BoundarySizeY;

        transform.position = position;
    }
}