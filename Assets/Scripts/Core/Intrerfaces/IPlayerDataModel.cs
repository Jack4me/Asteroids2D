using UniRx;
using UnityEngine;

namespace Core.Intrerfaces
{
    public interface IPlayerDataModel
    {
        public ReactiveProperty<Vector2> Position { get; }
        public ReactiveProperty<float> Speed { get;  }
        public ReactiveProperty<float> RotationAngle { get; } 
        public ReactiveProperty<int> Health { get; } 
        
        public ReactiveProperty<int> Score { get; }
    }
    
}
