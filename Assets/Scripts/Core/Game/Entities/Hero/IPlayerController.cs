using Core.Models;
using Core.Services.Intrerfaces;

namespace Core.Game.Entities.Hero
{
    public interface IPlayerController
    {
        public ILaserViewModel LaserViewModel { get; set; }
        public void Construct(IPlayerDataModel dataModel);
    }
}