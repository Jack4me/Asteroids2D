using Core.Services;
using Core.StaticData;

namespace Core.Intrerfaces.Services
{
    public interface ISpawnService : IService
    {
       
        public void RunAsyncMethods(SpawnPointsData spawnPointsData);
    }
}