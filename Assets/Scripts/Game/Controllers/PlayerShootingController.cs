using UnityEngine;

namespace Game.Controllers
{
    public class PlayerShootingController
    {
        private GameObject laserPrefab;
        private GameObject bulletPrefab;
        private Transform firePoint;
        private float laserCooldown;
        private float bulletCooldown;

        private float lastLaserFireTime;
        private float lastBulletFireTime;

        public PlayerShootingController(GameObject laserPrefab, GameObject bulletPrefab, Transform firePoint, float laserCooldown, float bulletCooldown)
        {
            this.laserPrefab = laserPrefab;
            this.bulletPrefab = bulletPrefab;
            this.firePoint = firePoint;
            this.laserCooldown = laserCooldown;
            this.bulletCooldown = bulletCooldown;

            lastLaserFireTime = 0f;
            lastBulletFireTime = 0f;
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

            Object.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            lastBulletFireTime = Time.time;
        }
    }
}
