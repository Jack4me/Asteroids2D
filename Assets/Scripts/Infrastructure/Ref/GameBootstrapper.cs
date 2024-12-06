using CodeBase.Infrastructure.States;
using Infrastructure.Ref.States;
using UnityEngine;

namespace CodeBase.Infrastructure {
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner {
        private Game _game;

        private void Awake(){
            _game = new Game(this);
            _game.StateMachine.EnterGeneric<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}