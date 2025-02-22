using Core.Services;
using Core.Services.Intrerfaces;
using UniRx;

namespace Core.Models
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public IReadOnlyReactiveProperty<string> PositionText { get; }
        public IReadOnlyReactiveProperty<string> SpeedText { get; }
        public IReadOnlyReactiveProperty<string> RotationText { get; }
        public IReadOnlyReactiveProperty<string> Health { get; }
        public IReadOnlyReactiveProperty<int> HealthInt { get; }
        public IReadOnlyReactiveProperty<string> Score { get; }
        private IPlayerDataModel model;

        public PlayerViewModel(IPlayerDataModel playerDataModel)
        {
            model = playerDataModel;
            PositionText = model.Position.Select(pos => $"Position: {pos.x:F2}, {pos.y:F2}").ToReactiveProperty();
            SpeedText = model.Speed.Select(speed => $"Speed: {speed:F2}").ToReactiveProperty();
            RotationText = model.RotationAngle.Select(angle => $"Rotation: {angle:F0}°").ToReactiveProperty();
            Health = model.Health.Select(health => $"Health: {health:F0}").ToReactiveProperty();
            HealthInt = model.Health.ToReactiveProperty();
            Score = model.Score.Select(score => $"{score:F0}").ToReactiveProperty();
        }
    }
}