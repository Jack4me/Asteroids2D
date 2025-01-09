using Core;
using Main.States;
using UnityEngine;
using Zenject;

namespace Main
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
                Debug.Log("NULL _gameInit");
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