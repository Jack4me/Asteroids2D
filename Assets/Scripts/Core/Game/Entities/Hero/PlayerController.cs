﻿using Core.Models;
using Core.Services.Intrerfaces;
using UnityEngine;

namespace Core.Game.Entities.Hero
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayerDataModel playerDataModel;
        private HeroInput _heroInput;
        public ILaserViewModel LaserViewModel { get; set; }

        public void Construct(IPlayerDataModel playerDataModel)
        {
            this.playerDataModel = playerDataModel;
            _heroInput = GetComponent<HeroInput>();
        }

        private void Update()
        {
            playerDataModel.Position.Value = transform.position;
            playerDataModel.RotationAngle.Value = transform.eulerAngles.z;
            playerDataModel.Speed.Value = _heroInput.heroMove.velocity.magnitude;
        }
    }
}