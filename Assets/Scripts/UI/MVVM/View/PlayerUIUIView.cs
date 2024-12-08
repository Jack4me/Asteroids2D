using System.Collections.Generic;
using Core.Intrerfaces;
using Core.Models;
using Game.Models;
using ModestTree;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.MVVM.View
{
    public class PlayerUIUIView : MonoBehaviour, IPlayerUIView
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

        public void Construct(IPlayerViewModel viewModelPlayer)
        {
            this._viewModelPlayer = viewModelPlayer;
        }

        // [Inject]
        // public void Construct(PlayerViewModel viewModelPlayer)
        // {
        //     this._viewModelPlayer = viewModelPlayer;
        // }
        // [Inject]
        // public void Construct(LaserViewModel viewModelLaser)
        // {
        //     _viewModelLaser = viewModelLaser;
        // }
        private void Start()

        {
            if (_viewModelPlayer == null)
                Debug.Log(_viewModelPlayer + "viewModel is null");

            _viewModelPlayer.PositionText.Subscribe(text => positionText.text = text).AddTo(this);
            _viewModelPlayer.SpeedText.Subscribe(text => speedText.text = text).AddTo(this);
            _viewModelPlayer.RotationText.Subscribe(text => rotationText.text = text).AddTo(this);
            _viewModelPlayer.Health.Subscribe(text => health.text = text).AddTo(this);

            _viewModelPlayer.HealthInt.Subscribe(UpdateHearts).AddTo(this);
            _viewModelPlayer.Score.Subscribe(text => score.text = text).AddTo(this);
            _viewModelLaser.LaserCount.Subscribe(count => { laserCountText.text = $"Lasers: {count}"; })
                .AddTo(this);

            _viewModelLaser.ReloadProgress.Subscribe(progress =>
            {
                reloadProgressText.text = $"Reload: {progress * 100:0}%";
            }).AddTo(this);
        }

        [SerializeField] private TextMeshProUGUI laserCountText; // UI-элемент для отображения количества лазеров
        [SerializeField] private TextMeshProUGUI reloadProgressText;


        private LaserViewModel _viewModelLaser;


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