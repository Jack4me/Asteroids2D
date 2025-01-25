using Core;

namespace Main
{
    public interface IJsonConfigLoader
    {
        public HeroMoveConfig LoadConfigsHero();
        public UFOConfig LoadConfigsEnemy();
        public LaserControllerConfig LoadConfigLaser();
    }
}