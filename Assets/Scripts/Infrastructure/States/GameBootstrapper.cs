using Core;
using Main;
using Main.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameInit _gameInit;

        [Inject]
        public void Construct(GameInit gameInit)
        {
            _gameInit = gameInit;
        }

        private void Awake()
        {
            if (_gameInit == null)
            {
                Debug.LogError("Null GameInit");
            }

            GameStateMachine stateMachine = _gameInit.StateMachine;
            if (stateMachine == null)
            {
                Debug.LogError("StateMachine is not initialized!");
                return;
            }

            stateMachine.EnterGeneric<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}