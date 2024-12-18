using Core.Models;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.MVVM.View
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI laserCountText; // UI-элемент для отображения количества лазеров
        [SerializeField] private TextMeshProUGUI reloadProgressText;
        
        
        private LaserViewModel _viewModel;
        
        [Inject]
        public void Construct(LaserViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        private void Start()
        {
            
            _viewModel.LaserCount.Subscribe(count => { laserCountText.text = $"Lasers: {count}"; })
                .AddTo(this);
        
            _viewModel.ReloadProgress.Subscribe(progress =>
            {
                reloadProgressText.text = $"Reload: {progress * 100:0}%";
            }).AddTo(this);
        }
    }
}