using Core.Game.Entities.Hero.Laser;
using Core.Intrerfaces;
using Core.Models;

namespace Core.Game.Entities.Hero
{
    public interface IPlayerController
    {
        public ILaserViewModel LaserViewModel { get; set; }
        public void Construct(IPlayerDataModel dataModel);
    }
}