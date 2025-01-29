using Core.Intrerfaces;
using UniRx;
using Zenject;

namespace Core.Game.Entities.Hero.Laser
{
    public class LaserViewModel : ILaserViewModel
    {
        public ReactiveProperty<int> LaserCount { get; set; }
        public ReactiveProperty<float> ReloadProgress { get; set; }
        private ILaserController _laserController;
        private readonly DiContainer _container;

        private CompositeDisposable _disposables = new CompositeDisposable();
        public LaserViewModel(ILaserController laserController)
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
                .AddTo(_disposables);
        }

        private void UpdateReloadProgress(float progress)
        {
            ReloadProgress.Value = progress;
        }
        public void Dispose()
        {
            _disposables.Dispose();
            _laserController.OnReloadProgress -= UpdateReloadProgress;
        }
    }
}