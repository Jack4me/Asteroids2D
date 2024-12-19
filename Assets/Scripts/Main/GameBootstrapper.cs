using CodeBase.Infrastructure;
using Core;
using Infrastructure.States;
using Main.States;
using UnityEngine;

namespace Infrastructure {
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner {
        private Game _game;

        private void Awake()
        {
            
            _game = new Game(this);
            _game.StateMachine.EnterGeneric<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}