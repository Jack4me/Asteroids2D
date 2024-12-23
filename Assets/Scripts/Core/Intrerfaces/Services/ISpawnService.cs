using CodeBase.Infrastructure.Services;
using Game;

namespace Core.Intrerfaces.Services
{
    public interface ISpawnService : IService
    {
       
        public void RunAsyncMethods(SpawnPointsData spawnPointsData);
    }
}