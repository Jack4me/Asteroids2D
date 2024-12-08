using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData;

namespace Core.StaticData {
    public interface IStaticDataService : IService {
        void Load();
        UnitConfig GetUnitConfig();
    }
}