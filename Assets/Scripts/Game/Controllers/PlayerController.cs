using Core.Factory;
using Core.Intrerfaces;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private IPlayerDataModel playerDataModel;
        private HeroMove _heroMove;

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