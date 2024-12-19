namespace Infrastructure.States {
    public class LoadProgressState : IState {
        private readonly GameStateMachine _gameStateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine){
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(){
            LoadProgressOrInitNew();
            _gameStateMachine.EnterGeneric<LoadLevelState, string>("MainScene");
        }

        public void Exit(){
        }

        private void LoadProgressOrInitNew(){
            // _persistentProgressService.Progress = _saveLoaderService.LoadProgress() ?? NewProgress();
            //_persistentProgressService.Progress = NewProgress();
        }
    }
}