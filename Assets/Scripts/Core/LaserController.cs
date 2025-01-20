using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class LaserController : MonoBehaviour
    {
        [SerializeField] public int maxLasers = 3;
        [SerializeField] private float reloadTime = 2f;
        private float reloadStartTime;
        public int currentLasers { get; private set; }
        public int CurrentLasers => currentLasers;
        public bool IsReloading => isReloading;
        private bool isReloading = false;
        public event Action<float> OnReloadProgress;

        private void Awake()
        {
            currentLasers = maxLasers;
        }

        public bool CanFireLaser()
        {
            return currentLasers > 0 && !isReloading;
        }

        public void UseLaser()
        {
            if (currentLasers > 0)
            {
                currentLasers--;
                if (currentLasers == 0)
                {
                    ReloadLasers().Forget();
                }
            }
        }

        private async UniTask ReloadLasers()
        {
            isReloading = true;
            reloadStartTime = Time.time;
            while (Time.time - reloadStartTime < reloadTime)
            {
                float progress = (Time.time - reloadStartTime) / reloadTime;
                OnReloadProgress?.Invoke(progress);
                await UniTask.Yield();
            }

            currentLasers = maxLasers;
            isReloading = false;
            OnReloadProgress?.Invoke(1.0f);
        }
    }
}