using System;
using System.Collections.Generic;
using Core.Intrerfaces;
using Infrastructure.Ref.Services;
using UnityEngine;

namespace Core
{
    public class ScoreManager : IScorable
    {
        private readonly IPlayerDataModel _playerDataModel;

        private Dictionary<EnemyType, int> scoreTable = new Dictionary<EnemyType, int>
        {
            { EnemyType.Small, 50 },
            { EnemyType.Medium, 20 },
            { EnemyType.Large, 10 },
            { EnemyType.Ufo, 100 }
        };

        public ScoreManager(IPlayerDataModel playerDataModel)
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