using Core.Intrerfaces;
using Core.Models;
using UnityEngine;

namespace Main
{
    public interface IHudFactory
    {
        GameObject CreateHud(LaserViewModel laserViewModel);
    }
}