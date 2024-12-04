using System.IO;
using UnityEngine;

namespace Game.Configs
{
    public class CreateJsonExample : MonoBehaviour
    {
        // Создаём данные
        void Start()
        {
            // Создаём данные
            PlayerConfig playerConfig = new PlayerConfig
            {
                health = 100,
                speed = 5.0f,
                weapon = new WeaponConfig
                {
                    damage = 20,
                    reloadTime = 1.5f
                }
            };

            // Преобразуем в JSON
            string json = JsonUtility.ToJson(playerConfig, true);

            // Указываем путь для сохранения
            string filePath = Path.Combine(Application.persistentDataPath, "PlayerConfig.json");

            // Сохраняем в файл
            File.WriteAllText(filePath, json);

            Debug.Log($"JSON-файл создан: {filePath}");
        }
    }
}
