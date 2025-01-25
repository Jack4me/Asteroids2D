using UniRx;
using Zenject;

namespace Core.Models
{
    public interface ILaserViewModel
    {
        public ReactiveProperty<int> LaserCount { get; set; }
        public ReactiveProperty<float> ReloadProgress { get; set; }
    }
}