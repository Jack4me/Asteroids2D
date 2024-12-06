using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using Infrastructure.Ref.Services;

namespace CodeBase.Infrastructure {
    public class Game {
        // public static IInputService InputService;
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner){
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
        }
    }
}