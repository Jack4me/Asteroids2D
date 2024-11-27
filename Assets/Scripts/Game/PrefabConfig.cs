using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "PrefabConfig", menuName = "Configs/PrefabConfig")]
    public class PrefabConfig : ScriptableObject
    {
        public GameObject asteroidPrefab;
        public GameObject mediumAsteroidPrefab;
        public GameObject smallAsteroidPrefab;
        public GameObject ufoPrefab;
    }
}

