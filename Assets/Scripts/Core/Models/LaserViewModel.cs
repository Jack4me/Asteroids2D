using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Models
{
    public class LaserViewModel
    {
        public ReactiveProperty<int> LaserCount { get; set; }
        public ReactiveProperty<float> ReloadProgress { get; set;}

        private LaserManager _laserManager;

        private readonly DiContainer _container;
        public LaserViewModel(LaserManager laserManager, DiContainer container = null)
        {
            _container = container;

            if (laserManager == null)
            {
                Debug.Log("PALUNDRA LASERMAN NULL");
            }
            _laserManager = laserManager;
            Debug.Log(laserManager.CurrentLasers + "_laserManager");
            UpdateProgressData();
        }

    

       public void UpdateProgressData()
        {
            LaserCount = new ReactiveProperty<int>(_laserManager.CurrentLasers);
            ReloadProgress = new ReactiveProperty<float>(1.0f);
            _laserManager.OnReloadProgress += UpdateReloadProgress;


            Observable.EveryUpdate()
                .Subscribe(_ => LaserCount.Value = _laserManager.CurrentLasers)
                .AddTo(_laserManager);
        }

        private void UpdateReloadProgress(float progress)
        {
            ReloadProgress.Value = progress;
        }
    }
}