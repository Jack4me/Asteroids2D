using System;
using System.ComponentModel;
using UnityEngine;

namespace UI.Test {
    public class MoneyStorage : MonoBehaviour {
        public event Action<int> OnStateChanged;

        public int Money { get; private set; }

        public void SetMoney(int money)
        {
            this.Money = money;
            this.OnStateChanged?.Invoke(this.Money);
        }

        public void AddMoney(int range)
        {
            this.Money += range;
            this.OnStateChanged?.Invoke(this.Money);
        }

        public void SpendMoney(int range)
        {
            this.Money -= range;
            this.OnStateChanged?.Invoke(this.Money);
        }
    }
}