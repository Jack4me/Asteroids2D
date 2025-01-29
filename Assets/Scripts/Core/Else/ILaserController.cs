using System;

namespace Core.Intrerfaces
{
    public interface ILaserController
    {
        int CurrentLasers { get; }
        public event Action<float> OnReloadProgress;
       bool CanFireLaser();
       void UseLaser();
    }
}