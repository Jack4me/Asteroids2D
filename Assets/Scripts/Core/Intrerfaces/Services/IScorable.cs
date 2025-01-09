namespace Core.Intrerfaces
{
    public interface IScorable
    {
        int GetTotalScore();
        void NotifyEnemyDestroyed(EnemyType enemyType);
    }
}

