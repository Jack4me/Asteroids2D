using Core;
using Core.Intrerfaces;
using UnityEngine;

namespace Game.Entities.Entities.UFO {
    public class UFO : Enemy {
        [SerializeField] private EnemyType size;
        public UFO(IObjectPool objectPool) : base(objectPool)
        {
        }
    }
}