using CodeBase.Infrastructure.Services;

namespace Core.Intrerfaces
{
    public interface IScorable : IService
    {
        int GetTotalScore();
        void NotifyEnemyDestroyed(EnemyType enemyType);
    }
}

