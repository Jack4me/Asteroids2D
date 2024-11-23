using UniRx;

namespace UI.Test.TestRocketMVVM
{
    public  class LaserViewModel
    {
        public ReactiveProperty<int> RocketCount { get; private set; }

        private LaserModel _model;

        public LaserViewModel(LaserModel model)
        {
            _model = model;
            RocketCount = new ReactiveProperty<int>(_model.RocketCount);
        }

        public void AddRocket(int count)
        {
            _model.AddRocket(count);
            RocketCount.Value = _model.RocketCount; // Обновляем реактивное свойство
        }

        public void UseRocket()
        {
            _model.UseRocket();
            RocketCount.Value = _model.RocketCount;
        }
    }
}