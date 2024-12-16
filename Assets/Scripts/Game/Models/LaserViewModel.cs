using UniRx;

namespace Game.Models
{
    public class LaserViewModel
    {
        public ReactiveProperty<int> LaserCount { get; private set; } // Количество лазеров
        public ReactiveProperty<float> ReloadProgress { get; private set; }

        private LaserManager _laserManager;

        public LaserViewModel(LaserManager laserManager)
        {
            _laserManager = laserManager;
            LaserCount = new ReactiveProperty<int>(_laserManager.CurrentLasers);
            ReloadProgress = new ReactiveProperty<float>(1.0f); // Начальное значение (перезарядка завершена)
            _laserManager.OnReloadProgress += UpdateReloadProgress;

            // Подписка на изменения
            Observable.EveryUpdate()
                .Subscribe(_ => LaserCount.Value = _laserManager.CurrentLasers)
                .AddTo(_laserManager);
        }

        public void UseLaser()
        {
            _laserManager.UseLaser();
            LaserCount.Value = _laserManager.CurrentLasers;
        }

        private void UpdateReloadProgress(float progress)
        {
            ReloadProgress.Value = progress;
        }
    }
}