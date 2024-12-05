using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace Ref_Code
{
    public class HeroAttack : MonoBehaviour
    {
        private IInputService _inputService;
    
        private void Awake(){
            _inputService = AllServices.Container.GetService<IInputService>();
            
               Debug.Log("_inputService"  + _inputService);
        }
        private void Update(){
            if (_inputService.IsAttackButtonUp()){
               Debug.Log("FIRE");
            }
            
        }
    }
}
