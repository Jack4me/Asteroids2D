using Core;
using Infrastructure.States;
using Main;
using Main.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
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