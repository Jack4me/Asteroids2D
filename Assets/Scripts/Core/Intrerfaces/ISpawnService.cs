using CodeBase.Infrastructure.Services;
using Game;

namespace Core.Intrerfaces
{
    public interface ISpawnService : IService
    {
       
        public void RunAsyncMethods(SpawnPointsData spawnPointsData);
    }
}