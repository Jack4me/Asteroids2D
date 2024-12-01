using Game.Models;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.MVVM.View
{
    public class PlayerUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI positionText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI rotationText;
        [SerializeField] private TextMeshProUGUI health;

        private PlayerViewModel viewModel;

        [Inject]
        public void Construct(PlayerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private void Start()
        {
            Debug.Log("viewModel.Health" + viewModel.Health);
            // Подписываем UI элементы на обновления данных
            viewModel.PositionText.Subscribe(text => positionText.text = text).AddTo(this);
            viewModel.SpeedText.Subscribe(text => speedText.text = text).AddTo(this);
            viewModel.RotationText.Subscribe(text => rotationText.text = text).AddTo(this);
            viewModel.Health.Subscribe(text => health.text = text).AddTo(this);
        }
    }
}