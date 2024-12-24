using Core;
using Infrastructure.States;
using Main.States;
using UnityEngine;

namespace Main {
    public class GameInit {
        public GameStateMachine StateMachine;
   
        public GameInit(GameStateMachine gameStateMachine)
        {
            StateMachine = gameStateMachine; 
        }
    }
}