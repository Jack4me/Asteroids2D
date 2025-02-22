using Core.Intrerfaces;
using Core.StaticData;
using Core.StaticData.Configs;
using UnityEngine;
using Zenject;

namespace Core.Game.Entities.Hero
{
    public class HeroLaser : MonoBehaviour
    {
        public float Speed = 20f;
        public float LifeTime = 3f;
        private LaserStatsConfig _laserConfig;

        [Inject] 
        public void Construct(IJsonConfigLoader jsonConfigLoader)
        {
            _laserConfig = jsonConfigLoader.LoadStatsConfigLaser();
        }
        private void Start()
        {
            if (_laserConfig != null)
            {
                Speed = _laserConfig.Speed;
                LifeTime = _laserConfig.Lifetime;
            }
            else
            {
                Debug.LogError("LaserConfig не загружен!");
            }
            Destroy(gameObject, LifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var target))
            {
                target.DestroyEntity();
            }
        }
        
    }
}