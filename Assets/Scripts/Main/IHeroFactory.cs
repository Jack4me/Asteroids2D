using Core;
using UnityEngine;

namespace Main
{
    public interface IHeroFactory
    {
        GameObject CreateHero(GameObject at, GameConfigs gameConfigs);
    }
}