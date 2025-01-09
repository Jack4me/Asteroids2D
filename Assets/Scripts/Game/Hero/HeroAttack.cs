using Core;
using Core.Intrerfaces.Services.Input;
using UnityEngine;

namespace Game.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private LaserController laserController;
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float laserCooldown;
        [SerializeField] private float bulletCooldown;
        private float lastLaserFireTime;
        private float lastBulletFireTime;
        private IInputService _inputService;
        private LaserController _laserController;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (_inputService.IsAttackBulletButton())
            {
                FireBullet();
            }

            if (_inputService.IsAttackLaserButton() && laserController.CanFireLaser())
            {
                FireLaser();
                laserController.UseLaser();
            }
        }

        public void FireLaser()
        {
            if (Time.time - lastLaserFireTime < laserCooldown) return;
            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
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