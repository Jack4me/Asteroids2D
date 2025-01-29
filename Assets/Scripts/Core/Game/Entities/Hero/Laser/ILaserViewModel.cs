using UniRx;

namespace Core.Game.Entities.Hero.Laser
{
    public interface ILaserViewModel
    {
        public ReactiveProperty<int> LaserCount { get; set; }
        public ReactiveProperty<float> ReloadProgress { get; set; }
    }
}