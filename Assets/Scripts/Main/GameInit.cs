using Core;
using Infrastructure.Ref.Services;
using Infrastructure.States;

namespace Main {
    public class GameInit {
        public GameStateMachine StateMachine;
        private SceneLoader sceneLoader;
        public GameInit(ICoroutineRunner coroutineRunner){
             sceneLoader = new SceneLoader(coroutineRunner);
            StateMachine = new GameStateMachine(sceneLoader);
        }
    }
}