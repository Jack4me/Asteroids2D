using System;
using System.Collections.Generic;
using Core.Intrerfaces;
using Game.Entities.Entities;
using Infrastructure.Ref.Services;
using UnityEngine;

namespace Core
{
  
    public class ScoreManager : MonoBehaviour
    {
       

        public event Action<int> OnScoreUpdated;
        private Dictionary<EnemyType, int> scoreTable = new Dictionary<EnemyType, int>
        {
            { EnemyType.Small, 50 },
            { EnemyType.Medium, 20 },
            { EnemyType.Large, 10 },
            { EnemyType.Ufo, 100 }
        };

        private IPlayerDataModel playerDataModel;
        private void Awake()
        {
            playerDataModel = AllServices.Container.GetService<IPlayerDataModel>();
        }
        public int totalScore { get; set; }
        
        
        public void NotifyEnemyDestroyed(EnemyType enemyType)
        {
            if (scoreTable.TryGetValue(enemyType, out int score))
            {
                totalScore += score;
                OnScoreUpdated?.Invoke(totalScore);
                Debug.Log($"Enemy of type {enemyType} destroyed. Added {score} points. Total Score: {totalScore}");
            }
            else
            {
                Debug.LogWarning($"No score defined for enemy type: {enemyType}");
            }
        }

        private void Update()
        {
            playerDataModel.Score.Value = GetTotalScore();

        }

        public int GetTotalScore()
        {
            return totalScore;
        }
    }
}
