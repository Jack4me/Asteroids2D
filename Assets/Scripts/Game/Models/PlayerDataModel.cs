using ModestTree;
using UniRx;
using UnityEngine;

namespace Game.Models
{
    public class PlayerDataModel
    {
        public ReactiveProperty<Vector2> Position { get; } = new ReactiveProperty<Vector2>();
        public ReactiveProperty<float> Speed { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<float> RotationAngle { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<int> Health { get; } = new ReactiveProperty<int>();
        
        public ReactiveProperty<int> Score { get; } = new ReactiveProperty<int>();
       }

}