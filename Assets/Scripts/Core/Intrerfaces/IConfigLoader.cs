using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IConfigLoader : IService

    {
    void ApplyPlayerConfig(GameObject player, PlayerConfig config);
    public void ApplyEnemyConfig(GameObject enemy, EnemyConfig config);
    }
}