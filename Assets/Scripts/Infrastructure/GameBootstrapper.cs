using CodeBase.Infrastructure;
using Infrastructure.States;
using UnityEngine;

namespace Core {
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner {
        private Game _game;

        private void Awake(){
            _game = new Game(this);
            _game.StateMachine.EnterGeneric<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}