using TMPro;
using UI.Test.TestRocketMVVM;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.MVVM.TestRocketMVVM
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI laserCountText; // UI-элемент для отображения количества лазеров

        private LaserViewModel _viewModel;

        [Inject]
        public void Construct(LaserViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Start()
        {
            // Подписка на изменения количества лазеров
            _viewModel.LaserCount.Subscribe(count => { laserCountText.text = $"Lasers: {count}"; })
                .AddTo(this); // Отписка при уничтожении объекта
        }
    }
}