using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Test.TestRocketMVVM
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rocketCountText; // UI-элемент для отображения

        private LaserViewModel _viewModel;

        [Inject]
        public void Construct(LaserViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Start()
        {
            // Подписываемся на изменения ReactiveProperty
            _viewModel.RocketCount.Subscribe(count => { rocketCountText.text = $"Rockets: {count}"; })
                .AddTo(this); // Отписка автоматом при уничтожении объекта
        }
    }
}