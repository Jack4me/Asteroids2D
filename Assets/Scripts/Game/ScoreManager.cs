using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum EnemyType
    {
        Small,
        Medium,
        Large,
        Ufo
    }
    public class ScoreManager : MonoBehaviour
    {
        private Dictionary<EnemyType, int> scoreTable = new Dictionary<EnemyType, int>
        {
            { EnemyType.Small, 10 },
            { EnemyType.Medium, 20 },
            { EnemyType.Large, 50 },
            { EnemyType.Ufo, 100 }
        };

        private int totalScore;

        public void AddScore(EnemyType enemyType)
        {
            if (scoreTable.TryGetValue(enemyType, out int score))
            {
                totalScore += score;
                Debug.Log($"Added {score} points for {enemyType}. Total Score: {totalScore}");
            }
            else
            {
                Debug.LogWarning($"No score defined for enemy type: {enemyType}");
            }
        }

        public int GetTotalScore()
        {
            return totalScore;
        }
    }
}
