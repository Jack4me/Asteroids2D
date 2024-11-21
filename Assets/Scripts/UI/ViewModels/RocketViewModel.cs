using System;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.ViewModels {
    public class RocketViewModel : IInitializable, IDisposable
    {
        private readonly CurrencyView rocketView;

        public RocketViewModel(CurrencyView rocketView) {
            this.rocketView = rocketView;
        }

        public void Initialize() {
           
        }

        public void Dispose() {
            
        }
    }
}
