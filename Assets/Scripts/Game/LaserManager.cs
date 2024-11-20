using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game {
    public class LaserManager : MonoBehaviour {
        [SerializeField] private int maxLasers = 3; 
        [SerializeField] private float reloadTime = 2f; 

        private int currentLasers; 
        private bool isReloading = false;

        public int CurrentLasers => currentLasers;
        public bool IsReloading => isReloading;

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
            await UniTask.Delay((int)(reloadTime * 1000));
            currentLasers = maxLasers;
            isReloading = false;
        }
    }
}