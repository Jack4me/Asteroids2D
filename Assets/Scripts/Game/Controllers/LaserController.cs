using System;
using Core;
using Core.Intrerfaces;
using Cysharp.Threading.Tasks;
using Main;
using UnityEngine;

namespace Game.Controllers
{
    public class LaserController  : ILaserController
    {
        [SerializeField] public int maxLasers = 3;
        [SerializeField] private float reloadTime = 2f;
        private float reloadStartTime;
        public int currentLasers { get; private set; }
        public int CurrentLasers => currentLasers;
        public bool IsReloading => isReloading;
        private bool isReloading = false;
        public event Action<float> OnReloadProgress;
        private IJsonConfigLoader _iJsonConfigLoader;
        private readonly LaserControllerConfig _config;

        
        
        public LaserController(IJsonConfigLoader iJsonConfigLoader)
        {
            _iJsonConfigLoader = iJsonConfigLoader;
           _config = _iJsonConfigLoader.LoadConfigLaser();
           maxLasers = _config.maxLasers;
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