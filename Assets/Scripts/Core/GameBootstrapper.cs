using CodeBase.Infrastructure;
using Core.States;
using UnityEngine;

namespace Core {
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner {
        private CodeBase.Infrastructure.Game _game;

        private void Awake(){
            _game = new CodeBase.Infrastructure.Game(this);
            _game.StateMachine.EnterGeneric<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}