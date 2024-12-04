using UnityEngine;

namespace CodeBase.Infrastructure.StaticData {
    public class StaticDataService : IStaticDataService {
        private UnitConfig _unitConfig;

        public UnitConfig GetUnitConfig(){
            return _unitConfig;
        }

        public void Load(){
            _unitConfig = Resources.Load<UnitStaticData>("StaticData/UnitStaticData").UnitConfig;
        }
    }
}