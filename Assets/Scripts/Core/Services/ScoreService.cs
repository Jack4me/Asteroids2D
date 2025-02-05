using System.Collections.Generic;
using Core.Services.Intrerfaces;

namespace Core.Services
{
    public class ScoreService : IScorable
    {
        private readonly IPlayerDataModel _playerDataModel;

        private Dictionary<EnemyType, int> scoreTable = new Dictionary<EnemyType, int>
        {
            { EnemyType.Small, 50 },
            { EnemyType.Medium, 20 },
            { EnemyType.Large, 10 },
            { EnemyType.Ufo, 100 }
        };

        public ScoreService(IPlayerDataModel playerDataModel)
        {
            _playerDataModel = playerDataModel;
        }

        public int totalScore { get; set; }

        public void NotifyEnemyDestroyed(EnemyType enemyType)
        {
            if (scoreTable.TryGetValue(enemyType, out int score))
            {
                totalScore += score;
            }

            _playerDataModel.Score.Value = GetTotalScore();
        }

        public int GetTotalScore()
        {
            return totalScore;
        }
    }
}