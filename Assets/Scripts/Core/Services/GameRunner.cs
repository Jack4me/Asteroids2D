using CodeBase.Infrastructure;
using UnityEngine;

namespace Core.Services {
    public class GameRunner : MonoBehaviour {
        public GameBootstrapper bootstrapper;

        private void Awake(){
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            if (bootstrapper == null) Instantiate(this.bootstrapper);
        }
    }
}