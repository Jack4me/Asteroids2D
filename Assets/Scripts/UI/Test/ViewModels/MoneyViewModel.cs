using System;
using UI.Test.View;
using UniRx;
using Zenject;

namespace UI.Test.ViewModels {
    public class MoneyViewModel : IInitializable, IDisposable
    {
        
        public readonly ReactiveProperty<string> RocketCount = new();
        public readonly MoneyStorage moneyStorage;
        

        public MoneyViewModel(MoneyStorage moneyStorage) {
            this.moneyStorage = moneyStorage;
        }

        public void Initialize() {
            OnMoneyChanged(moneyStorage.Money);
            moneyStorage.OnStateChanged += this.OnMoneyChanged;
        }


        public void Dispose() {
            moneyStorage.OnStateChanged -= this.OnMoneyChanged;

        }
        private void OnMoneyChanged(int money) {
            RocketCount.Value = money + "$";
        }
    }
}
