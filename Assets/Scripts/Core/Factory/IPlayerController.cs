using Core.Intrerfaces;
using Core.Models;

namespace Core.Factory
{
    public interface IPlayerController
    {
        public ILaserViewModel LaserViewModel { get; set; }
        public void Construct(IPlayerDataModel dataModel);
    }
}