using UniRx;
using Zenject;

namespace Core.Models
{
    public class LaserViewModel
    {
        public ReactiveProperty<int> LaserCount { get; set; }
        public ReactiveProperty<float> ReloadProgress { get; set; }
        private LaserController _laserController;
        private readonly DiContainer _container;

        public LaserViewModel(LaserController laserController)
        {
            _laserController = laserController;
            UpdateProgressData();
        }

        public void UpdateProgressData()
        {
            LaserCount = new ReactiveProperty<int>(_laserController.CurrentLasers);
            ReloadProgress = new ReactiveProperty<float>(1.0f);
            _laserController.OnReloadProgress += UpdateReloadProgress;
            Observable.EveryUpdate()
                .Subscribe(_ => LaserCount.Value = _laserController.CurrentLasers)
                .AddTo(_laserController);
        }

        private void UpdateReloadProgress(float progress)
        {
            ReloadProgress.Value = progress;
        }
    }
}