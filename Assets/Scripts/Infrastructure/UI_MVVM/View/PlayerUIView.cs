using System.Collections.Generic;
using Core.Intrerfaces;
using Infrastructure.Ref.Services;
using TMPro;
using UniRx;
using UnityEngine;

namespace Infrastructure.UI_MVVM.View
{
    public class PlayerUIView : MonoBehaviour, IPlayerUIView
    {
        [SerializeField] private TextMeshProUGUI positionText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI rotationText;
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private Transform heartsContainer; // Контейнер для сердечек
        [SerializeField] private GameObject heartPrefab; // Префаб сердечка

        private IPlayerViewModel _viewModelPlayer;
        private List<GameObject> hearts = new List<GameObject>();

      

       
        private void Start()

        {
            _viewModelPlayer = AllServices.Container.GetService<IPlayerViewModel>();

            _viewModelPlayer.PositionText.Subscribe(text => positionText.text = text).AddTo(this);
            _viewModelPlayer.SpeedText.Subscribe(text => speedText.text = text).AddTo(this);
            _viewModelPlayer.RotationText.Subscribe(text => rotationText.text = text).AddTo(this);
            _viewModelPlayer.Health.Subscribe(text => health.text = text).AddTo(this);

            _viewModelPlayer.HealthInt.Subscribe(UpdateHearts).AddTo(this);
            _viewModelPlayer.Score.Subscribe(text => score.text = text).AddTo(this);
            
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