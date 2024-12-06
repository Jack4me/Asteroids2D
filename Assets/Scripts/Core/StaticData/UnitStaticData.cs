using UnityEngine;

namespace CodeBase.Infrastructure.StaticData {
    [CreateAssetMenu(fileName = "UnitStaticData", menuName = "StaticData/Unit", order = 0)]
    public class UnitStaticData : ScriptableObject {
        public UnitConfig UnitConfig;
    }
}