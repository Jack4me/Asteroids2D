using UnityEngine;

namespace Core.StaticData {
            [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject {
        public EnemyType AsteroidEnum;
        [Range(1, 100)] public int Hp;
        [Range(1, 30)] public int Damage;
        public int MoveSpeed;
        public GameObject Prefab;
    }
}