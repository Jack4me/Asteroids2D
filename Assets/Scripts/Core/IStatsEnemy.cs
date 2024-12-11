using UnityEngine;

namespace Core
{
    public interface IStatsEnemy
    {
        int Damage { get; set; }
        int Health { get; set; }
        float Speed { get; set; }

    }
}