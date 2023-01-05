
using LoadingScreen;

namespace Game_state_machine
{
    public class MainMenuState: IState
    {
        private readonly ILoadingScreen _loadingScreen;
        
        public MainMenuState (ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }
        
        
        public void Enter()
        {
            _loadingScreen.Hide();
        }

        public void Exit()
        {
            
        }
    }
}