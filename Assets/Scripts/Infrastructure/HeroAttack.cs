using Infrastructure.Ref.Services;
using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class HeroAttack : MonoBehaviour
    {
        private IInputService _inputService;
    
        private void Awake(){
            _inputService = AllServices.Container.GetService<IInputService>();
            
         
        }
        private void Update(){
            if (_inputService.IsAttackButtonUp()){
                Debug.Log("FIRE");
            }
            
        }
    }
}