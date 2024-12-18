using Core;
using Core.Intrerfaces.Services.Input;
using Infrastructure.Ref.Services;
using UnityEngine;
using Zenject;

namespace Game.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        private LaserManager laserManager;
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float laserCooldown;
        [SerializeField] private float bulletCooldown;

        [SerializeField] private float lastLaserFireTime;
        [SerializeField] private float lastBulletFireTime;
        private IInputService _inputService;

         private LaserManager _laserManager;

        private void Start()
        {
        }

        private void Awake()
        {
            _inputService = AllServices.Container.GetService<IInputService>();
            laserManager = FindObjectOfType<LaserManager>();
            if (_laserManager != null)
            {
                Debug.Log("LaserManager успешно инжектирован!");
            }
        }

        private void Update()
        {
            if (_inputService.IsAttackBulletButton())
            {
                FireBullet();
            }

            if (_inputService.IsAttackLaserButton() && laserManager.CanFireLaser())
            {
                FireLaser();
                laserManager.UseLaser();
            }
        }

        public void FireLaser()
        {
            if (Time.time - lastLaserFireTime < laserCooldown) return;

            Object.Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
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