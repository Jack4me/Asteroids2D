using Core.Intrerfaces;
using Core.Intrerfaces.Services.Input;
using Core.StaticData;
using UnityEngine;
using Zenject;

namespace Core.Game.Entities.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float laserCooldown;
        [SerializeField] private float bulletCooldown;
        private float lastLaserFireTime;
        private float lastBulletFireTime;
        private IInputService _inputService;
        private ILaserController _laserController;
        private DiContainer _container;
        private IJsonConfigLoader _jsonConfigLoader;

        public void Construct(IInputService inputService, ILaserController laserController, DiContainer container)
        {
            _inputService = inputService;
            _laserController = laserController;
            _container = container;
        }

        private void Update()
        {
            if (_inputService.IsAttackBulletButton())
            {
                FireBullet();
            }

            if (_inputService.IsAttackLaserButton() && _laserController.CanFireLaser())
            {
                FireLaser();
                _laserController.UseLaser();
            }
        }

        public void FireLaser()
        {
            if (Time.time - lastLaserFireTime < laserCooldown) return;
            //Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            _container.InstantiatePrefab(laserPrefab, firePoint.position, firePoint.rotation, transform);
            lastLaserFireTime = Time.time;
        }

        public void FireBullet()
        {
            if (Time.time - lastBulletFireTime < bulletCooldown) return;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            lastBulletFireTime = Time.time;
        }
    }
}