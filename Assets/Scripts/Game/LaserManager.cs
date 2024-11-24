using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game {
    public class LaserManager : MonoBehaviour {
        [SerializeField] private int maxLasers = 3; 
        [SerializeField] private float reloadTime = 2f; 
        private float reloadStartTime; 
        private int currentLasers; 
        private bool isReloading = false;

        public int CurrentLasers => currentLasers;
        public bool IsReloading => isReloading;

        public event Action<float> OnReloadProgress;
        private void Start() {
            currentLasers = maxLasers;
        }

        public bool CanFireLaser() {
            return currentLasers > 0 && !isReloading;
        }

        public void UseLaser() {
            Debug.Log("currentLasers" + currentLasers);
            if (currentLasers > 0) {
                currentLasers--;
                if (currentLasers == 0) {
            Debug.Log("reload" );
                    ReloadLasers().Forget();
                }
            }
        }

        private async UniTask ReloadLasers() {
            isReloading = true;
            reloadStartTime = Time.time;

            while (Time.time - reloadStartTime < reloadTime) {
                float progress = (Time.time - reloadStartTime) / reloadTime;
                OnReloadProgress?.Invoke(progress); // Передаем прогресс перезарядки
                await UniTask.Yield();
            }

            currentLasers = maxLasers;
            isReloading = false;
            OnReloadProgress?.Invoke(1.0f); // Завершаем прогресс
        }
    }
}