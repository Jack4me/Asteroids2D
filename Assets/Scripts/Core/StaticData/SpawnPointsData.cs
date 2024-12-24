using UnityEngine;

namespace Core.StaticData
{
    [CreateAssetMenu(fileName = "SpawnPointsData", menuName = "SpawnPoints")]
    public class SpawnPointsData : ScriptableObject
    {
        public Vector3[] spawnPositions;
    }
}