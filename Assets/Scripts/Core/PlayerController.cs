using System;
using Core.Intrerfaces;
using UnityEngine;
using Zenject;

namespace Core
{
    public class PlayerController : MonoBehaviour
    {
       

        private IPlayerDataModel playerDataModel;


        public void Construct(IPlayerDataModel playerDataModel)
        {
            this.playerDataModel = playerDataModel;
        }


        private void Update()
        {
            playerDataModel.Position.Value = transform.position;
            playerDataModel.RotationAngle.Value = transform.eulerAngles.z;
        }
    }
}