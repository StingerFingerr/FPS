using Scene_service;

namespace Game_state_machine
{
    public class LoadMainMenuState: IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        private readonly ISceneLoader _sceneLoader;

        public LoadMainMenuState (
            ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine
        )
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
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