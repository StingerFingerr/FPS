using Scene_service;
using UnityEngine;

namespace Game_state_machine
{
    public class LoadLevelState: IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState (
            ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine
        )
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadSceneAsync("City");
        }

        public void Exit()
        {
            
        }
    }
}