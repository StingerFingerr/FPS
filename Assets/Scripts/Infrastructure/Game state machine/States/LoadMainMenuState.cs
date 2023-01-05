using Scene_service;
using Zenject;

namespace Game_state_machine
{
    public class LoadMainMenuState: IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadMainMenuState (
            ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter() => 
            _sceneLoader.LoadSceneAsync("Menu", EnterNextState);

        private void EnterNextState() => 
            _gameStateMachine.Enter<MainMenuState>();

        public void Exit()
        {
            
        }
    }
}