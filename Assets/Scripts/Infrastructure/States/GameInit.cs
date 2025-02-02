using Main.States;

namespace Main
{
    public class GameInit
    {
        public GameStateMachine StateMachine;

        public GameInit(GameStateMachine gameStateMachine)
        {
            StateMachine = gameStateMachine;
        }
    }
}