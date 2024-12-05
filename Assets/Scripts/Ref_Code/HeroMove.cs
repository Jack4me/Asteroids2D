using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace Ref_Code
{
    public class HeroMove : MonoBehaviour
    {
        public float movementSpeed = 4.0f;
        private IInputService _inputService;
        private Camera _camera;

        public HeroMove Construct(){
            return this;
        }

        private void Awake(){
            _inputService = AllServices.Container.GetService<IInputService>();
        }
        private void Update(){
            Vector3 movementVector = Vector3.zero;
            if (_inputService.Axis.sqrMagnitude > 0.01f){
                Debug.Log("_inputService.Axis" + _inputService.Axis);
            }
           
        }

    }
}
