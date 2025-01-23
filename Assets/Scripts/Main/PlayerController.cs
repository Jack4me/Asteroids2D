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
        private HeroInput _heroInput;
        public LaserViewModel LaserViewModel { get; set; }

        public void Construct(IPlayerDataModel playerDataModel)
        {
            this.playerDataModel = playerDataModel;
            _heroInput = GetComponent<HeroInput>();
        }

        private void Update()
        {
            playerDataModel.Position.Value = transform.position;
            playerDataModel.RotationAngle.Value = transform.eulerAngles.z;
            playerDataModel.Speed.Value = _heroInput.velocity.magnitude;
        }
    }
}