using UniRx;

namespace Game.MVVM.TestRocketMVVM
{
    public class LaserViewModel
    {
        public ReactiveProperty<int> LaserCount { get; private set; } // Количество лазеров

        private LaserManager _manager;

        public LaserViewModel(LaserManager manager)
        {
            _manager = manager;
            LaserCount = new ReactiveProperty<int>(_manager.CurrentLasers);

            // Подписка на изменения
            Observable.EveryUpdate()
                .Subscribe(_ => LaserCount.Value = _manager.CurrentLasers)
                .AddTo(_manager);
        }

        public bool CanFireLaser() => _manager.CanFireLaser();

        public void UseLaser() => _manager.UseLaser();
    }
}