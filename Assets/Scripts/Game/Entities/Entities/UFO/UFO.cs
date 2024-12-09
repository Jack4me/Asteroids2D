using Core;
using Core.Intrerfaces;
using UnityEngine;

namespace Game.Entities.Entities.Enemies {
    public class UFO : Enemy {
        [SerializeField] private EnemyType size;
        public UFO(IObjectPool objectPool) : base(objectPool)
        {
        }
    }
}