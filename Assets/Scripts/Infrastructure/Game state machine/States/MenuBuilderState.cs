
using LoadingScreen;

namespace Game_state_machine
{
    public class MenuBuilderState: IState
    {
        private readonly ILoadingScreen _loadingScreen;
        
        public MenuBuilderState (ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }

        public void Enter()
        {
            _loadingScreen.Hide();
        }

        public void Exit()
        {
            _loadingScreen.Show();
        }
    }
}