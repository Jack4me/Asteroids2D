using System;
using Core.Intrerfaces;
using Core.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Game.Entities.Hero
{
    public class LaserController : ILaserController
    {
        public int CurrentLasers => currentLasers;
        private int maxLasers = 3;
        private float reloadTime = 2f;
        private float reloadStartTime;
        private bool isReloading = false;
        private IJsonConfigLoader _iJsonConfigLoader;
        private readonly LaserControllerConfig _config;
        public int currentLasers { get; private set; }
        public event Action<float> OnReloadProgress;

        public LaserController(IJsonConfigLoader iJsonConfigLoader)
        {
            _iJsonConfigLoader = iJsonConfigLoader;
            _config = _iJsonConfigLoader.LoadConfigLaser();
            currentLasers = _config.maxLasers;
            reloadTime = _config.reloadTime;
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