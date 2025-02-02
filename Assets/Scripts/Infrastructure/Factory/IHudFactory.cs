using Core.Models;
using UnityEngine;

namespace Main
{
    public interface IHudFactory
    {
        GameObject CreateHud(ILaserViewModel laserViewModel);
    }
}