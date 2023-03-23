using Scene_service;

namespace Game_state_machine
{
    public class InitialState: IState
    { 
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public InitialState (
            ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter() => 
            _sceneLoader.LoadSceneAsync("Menu", EnterNextState);

        private void EnterNextState() => 
            _gameStateMachine.Enter<MenuBuilderState>();

        public void Exit()
        {
            
        }
    }
}