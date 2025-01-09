using Core.Models;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Infrastructure.UI.View
{
    public class LaserUIController : MonoBehaviour
    {
        [SerializeField] private Transform missileIconContainer;
        [SerializeField] private GameObject missileIconPrefab;
        [SerializeField] private Image reloadCircle;
        private LaserViewModel _viewModel;

        private void Start()
        {
            reloadCircle.gameObject.SetActive(false);
            _viewModel.ReloadProgress
                .Subscribe(progress =>
                {
                    if (progress < 1.0f)
                    {
                        reloadCircle.gameObject.SetActive(true);
                        reloadCircle.fillAmount = progress;
                    }
                    else
                    {
                        reloadCircle.gameObject.SetActive(false);
                    }
                })
                .AddTo(this);
        }

        public void Initialize(LaserViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.LaserCount
                .Subscribe(UpdateMissileIcons)
                .AddTo(this);
            _viewModel.ReloadProgress
                .Subscribe(UpdateReloadCircle)
                .AddTo(this);
        }

        private void UpdateMissileIcons(int count)
        {
            foreach (Transform child in missileIconContainer)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < count; i++)
            {
                Instantiate(missileIconPrefab, missileIconContainer);
            }
        }

        private void UpdateReloadCircle(float progress)
        {
            reloadCircle.fillAmount = progress;
        }
    }
}