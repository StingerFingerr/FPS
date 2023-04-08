using LoadingScreen;
using UnityEngine;

namespace Game_state_machine
{
    public class GameLoopState: IState
    {
        private readonly ILoadingScreen _loadingScreen;

        public GameLoopState (ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }
        
        public void Enter()
        {
            _loadingScreen.Hide();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Exit()
        {
            
        }
    }
}