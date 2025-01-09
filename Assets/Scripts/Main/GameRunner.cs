using UnityEngine;

namespace Main {
    public class GameRunner : MonoBehaviour {
        public GameBootstrapper bootstrapper;

        private void Awake(){
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            if (bootstrapper == null) Instantiate(this.bootstrapper);
        }
    }
}