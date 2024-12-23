using Core;
using Infrastructure.States;
using UnityEngine;

namespace Main {
    public class GameInit {
        public GameStateMachine StateMachine;
   
        public GameInit(GameStateMachine gameStateMachine)
        {
            Debug.Log("GAME INIT");
            StateMachine = gameStateMachine; 
        }
    }
}