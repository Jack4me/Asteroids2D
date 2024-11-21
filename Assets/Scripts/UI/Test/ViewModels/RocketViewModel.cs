using System;
using UI.Test.View;
using UniRx;
using Zenject;

namespace UI.Test.ViewModels {
    public class RocketViewModel : IInitializable, IDisposable
    {
        
        private readonly CurrencyView rocketView;
        private readonly ReactiveProperty<string> rocketCount = new();

        public RocketViewModel(CurrencyView rocketView) {
            this.rocketView = rocketView;
        }

        public void Initialize() {
           
        }

        public void Dispose() {
            
        }
    }
}
