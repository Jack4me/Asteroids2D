using System.IO;
using Infrastructure.Config;
using UnityEngine;

namespace Game.Config
{
    [System.Serializable]
    public class PlayerConfig
    {
        public int health;
        public float speed;
        public string weaponName;
    }

    [System.Serializable]
    public class EnemyConfig
    {
        public int health;
        public float speed;
        public int damage;
    }

    [System.Serializable]
    public class GameConfigs
    {
        public PlayerConfig player;
        public EnemyConfig enemy;
    }

    public class ConfigLoader : MonoBehaviour
    {
        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject enemyObject;

        private void Start()
        {
           
            
            // Загружаем JSON
            string filePath = Path.Combine(Application.dataPath, "Configs.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                GameConfigs configs = JsonUtility.FromJson<GameConfigs>(json);

                // Применяем параметры к игроку
                ApplyPlayerConfig(playerObject, configs.player);

                // Применяем параметры к врагу
                // ApplyEnemyConfig(enemyObject, configs.enemy);
            }
            else
            {
                Debug.LogWarning("Файл JSON не найден!");
            }
        }

        private void ApplyPlayerConfig(GameObject player, PlayerConfig config)
        {
            PlayerStats stats = player.GetComponent<PlayerStats>();
            stats.health = config.health;
            stats.speed = config.speed;
            stats.weaponName = config.weaponName;
        }

        private void ApplyEnemyConfig(GameObject enemy, EnemyConfig config)
        {
            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            stats.health = config.health;
            stats.speed = config.speed;
            stats.damage = config.damage;
        }
    }
}