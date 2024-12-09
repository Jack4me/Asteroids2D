using Core.Factory;
using Core.Intrerfaces;
using Game.Config;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayerDataModel playerDataModel;
        private IConfigLoader _configLoader;
        private HeroMove _heroMove;

        public void Construct(IPlayerDataModel playerDataModel, IConfigLoader configLoader)
        {
            this.playerDataModel = playerDataModel;
            _configLoader = configLoader;
            _heroMove = GetComponent<HeroMove>();
            GetComponent<PlayerStats>().Constarct(_configLoader);
        }


        private void Update()
        {
            playerDataModel.Position.Value = transform.position;
            playerDataModel.RotationAngle.Value = transform.eulerAngles.z;
            playerDataModel.Speed.Value = _heroMove.velocity.magnitude;
            
        }
    }
}