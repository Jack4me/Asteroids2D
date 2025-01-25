using Core.Models;
using TMPro;
using UniRx;
using UnityEngine;

namespace Infrastructure.UI.View
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI laserCountText;
        [SerializeField] private TextMeshProUGUI reloadProgressText;
        private ILaserViewModel _viewModel;

        public void Construct(ILaserViewModel viewModel)
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