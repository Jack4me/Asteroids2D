using CodeBase.Infrastructure;
using Core.Intrerfaces;
using Core.States;
using Infrastructure.Ref.Services;
using UnityEngine;
using Zenject;

namespace Core {
    public class Game {
        // public static IInputService InputService;
        public GameStateMachine StateMachine;
        
        public Game(ICoroutineRunner coroutineRunner){
            

            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
        }
    }
}