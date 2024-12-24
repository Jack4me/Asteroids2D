using Infrastructure.States;

namespace Main.States {
    public class LoadProgressState : IState {
        private readonly GameStateMachine _gameStateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine){
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(){
            _gameStateMachine.EnterGeneric<LoadLevelState, string>("MainScene");
        }

        public void Exit(){
        }
      
    }
}