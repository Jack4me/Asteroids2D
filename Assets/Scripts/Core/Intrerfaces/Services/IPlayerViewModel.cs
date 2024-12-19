using CodeBase.Infrastructure.Services;
using UniRx;

namespace Core.Intrerfaces
{
    public interface IPlayerViewModel : IService
    {
        public IReadOnlyReactiveProperty<string> PositionText { get; }
        public IReadOnlyReactiveProperty<string> SpeedText { get; }
        public IReadOnlyReactiveProperty<string> RotationText { get; }
        public IReadOnlyReactiveProperty<string> Health { get; }
        public IReadOnlyReactiveProperty<int> HealthInt { get; }
        public IReadOnlyReactiveProperty<string> Score { get; }
    }
}