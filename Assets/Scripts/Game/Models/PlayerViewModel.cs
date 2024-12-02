using UniRx;
using UnityEngine;

namespace Game.Models
{
    public class PlayerViewModel
    {
        public IReadOnlyReactiveProperty<string> PositionText { get; }
        public IReadOnlyReactiveProperty<string> SpeedText { get; }
        public IReadOnlyReactiveProperty<string> RotationText { get; }
        public IReadOnlyReactiveProperty<string> Health { get; }
        public IReadOnlyReactiveProperty<int> HealthInt { get; }

        public PlayerViewModel(PlayerDataModel model)
        {
            // Преобразуем данные модели в строки для UI
            PositionText = model.Position.Select(pos => $"Position: {pos.x:F2}, {pos.y:F2}").ToReactiveProperty();
            SpeedText = model.Speed.Select(speed => $"Speed: {speed:F2}").ToReactiveProperty();
            RotationText = model.RotationAngle.Select(angle => $"Rotation: {angle:F0}°").ToReactiveProperty();
            Health = model.Health.Select(health => $"Health: {health:F0}").ToReactiveProperty();
            HealthInt = model.Health.ToReactiveProperty();
        }
    }
}