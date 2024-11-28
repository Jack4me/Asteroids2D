using Core;
using Core.Intrerfaces;
using UnityEngine;

namespace Game.Entities.Entities.Enemies {
    public class Enemy : Entity {
        public Enemy(IObjectPool objectPool) : base(objectPool)
        {
        }
    }
}