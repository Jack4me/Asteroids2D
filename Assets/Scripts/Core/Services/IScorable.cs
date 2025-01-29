namespace Core.Services
{
    public interface IScorable
    {
        int GetTotalScore();
        void NotifyEnemyDestroyed(EnemyType enemyType);
    }
}

