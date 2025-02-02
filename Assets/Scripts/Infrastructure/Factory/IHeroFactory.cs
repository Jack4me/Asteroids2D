using Core;
using Core.StaticData;
using UnityEngine;

namespace Main
{
    public interface IHeroFactory
    {
        GameObject CreateHero(GameObject at, HeroMoveConfig heroMoveConfig);
    }
}