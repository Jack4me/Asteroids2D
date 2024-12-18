using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AdvancedSpawnConfig", menuName = "Configs/AdvancedSpawnConfig")]
    public class AdvancedSpawnConfig : ScriptableObject
    {
        [Header("Prefab to Spawn")]
        public GameObject prefab; // Префаб объекта

        [Header("Pool Parent")]
        public Transform poolParent; // Контейнер для пула

        [Header("Spawn Points")]
        public List<Transform> spawnPoints; // Готовые точки спавна
    }
}