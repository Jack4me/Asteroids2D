using Core.Factory;
using Core.Intrerfaces;
using Core.Models;
using Game.Controllers;
using UnityEngine;

namespace Main
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayerDataModel playerDataModel;
        private HeroMove _heroMove;

        public LaserViewModel LaserViewModel { get; set; }

        public void Construct(IPlayerDataModel playerDataModel)
        {
            this.playerDataModel = playerDataModel;
            _heroMove = GetComponent<HeroMove>();
        }


        private void Update()
        {
            playerDataModel.Position.Value = transform.position;
            playerDataModel.RotationAngle.Value = transform.eulerAngles.z;
            playerDataModel.Speed.Value = _heroMove.velocity.magnitude;
            
        }
    }
}