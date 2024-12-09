using System.IO;
using Core.Intrerfaces;
using Game.Config;
using UnityEngine;

namespace Core
{
    public class ConfigLoader : IConfigLoader
    {
        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject enemyObject;

        private void Start()
        {
            
            LoadJsonFile();
        }

        public void LoadJsonFile()
        {
            string filePath = Path.Combine(Application.dataPath, "Configs.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                GameConfigs configs = JsonUtility.FromJson<GameConfigs>(json);

                // Применяем параметры к игроку
              //  ApplyPlayerConfig(playerObject, configs.player);

                // Применяем параметры к врагу
                // ApplyEnemyConfig(enemyObject, configs.enemy);
            }
            else
            {
                Debug.LogWarning("Файл JSON не найден!");
            }
        }

        // public void ApplyPlayerConfig(GameObject player, PlayerConfig config)
        // {
        //   PlayerStats stats = player.GetComponent<PlayerStats>();
        //     stats.health = config.health;
        //     stats.speed = config.speed;
        //     stats.weaponName = config.weaponName;
        // }
        //
        // public void ApplyEnemyConfig(GameObject enemy, EnemyConfig config)
        // {
        //    EnemyStats stats = enemy.GetComponent<EnemyStats>();
        //     stats.health = config.health;
        //     stats.speed = config.speed;
        //     stats.damage = config.damage;
        // }
        //
        //
        public void ApplyPlayerConfig(GameObject player, PlayerConfig config)
        {
            throw new System.NotImplementedException();
        }

        public void ApplyEnemyConfig(GameObject enemy, EnemyConfig config)
        {
            throw new System.NotImplementedException();
        }
    }
}