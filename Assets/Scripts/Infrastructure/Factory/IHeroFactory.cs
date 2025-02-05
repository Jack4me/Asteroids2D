using Core.StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IHeroFactory
    {
        GameObject CreateHero(GameObject at, HeroMoveConfig heroMoveConfig);
    }
}