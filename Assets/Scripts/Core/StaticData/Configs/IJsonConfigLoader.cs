namespace Core.StaticData
{
    public interface IJsonConfigLoader
    {
        public HeroMoveConfig LoadConfigsHero();
        public UFOConfig LoadConfigsEnemy();
        public LaserControllerConfig LoadConfigLaser();
        public LaserStatsConfig LoadStatsConfigLaser();
    }
}