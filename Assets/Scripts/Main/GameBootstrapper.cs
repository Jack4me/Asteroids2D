using Core;
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
            _gameInit.StateMachine.EnterGeneric<BootStrapState>();
            DontDestroyOnLoad(this);
        }
    }
}