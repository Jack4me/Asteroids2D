using CodeBase.Infrastructure.Services;

namespace Core.Intrerfaces
{
    public interface ISpawnService : IService
    {
        public void SpawnAsteroid();
    }
}