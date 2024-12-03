using System.Collections.Generic;
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
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private Transform heartsContainer; // Контейнер для сердечек
        [SerializeField] private GameObject heartPrefab; // Префаб сердечка

        private PlayerViewModel viewModel;
        private List<GameObject> hearts = new List<GameObject>();

        [Inject]
        public void Construct(PlayerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        private void Start()
        {

            viewModel.PositionText.Subscribe(text => positionText.text = text).AddTo(this);
            viewModel.SpeedText.Subscribe(text => speedText.text = text).AddTo(this);
            viewModel.RotationText.Subscribe(text => rotationText.text = text).AddTo(this);
            viewModel.Health.Subscribe(text => health.text = text).AddTo(this);
          
            viewModel.HealthInt.Subscribe(UpdateHearts).AddTo(this);
            viewModel.Score.Subscribe(text => score.text = text).AddTo(this);
         
        }

        private void UpdateHearts(int health)
        {
            while (hearts.Count > health)
            {
                Destroy(hearts[0]); 
                hearts.RemoveAt(0);
            }

            // Добавляем недостающие сердечки (справа налево)
            while (hearts.Count < health)
            {
                GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
                hearts.Add(newHeart);
            }

            
        }
    }
}