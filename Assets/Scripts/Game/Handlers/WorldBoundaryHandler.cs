using UnityEngine;

public class WorldBoundaryHandler : MonoBehaviour {
    private const float BoundarySizeX = 12.5f; 
    private const float BoundarySizeY = 7f; 

    private void LateUpdate() {
        Vector3 position = transform.position;

        if (position.x > BoundarySizeX) position.x = -BoundarySizeX;
        else if (position.x < -BoundarySizeX) position.x = BoundarySizeX;

        if (position.y > BoundarySizeY) position.y = -BoundarySizeY;
        else if (position.y < -BoundarySizeY) position.y = BoundarySizeY;

        transform.position = position;
    }
}