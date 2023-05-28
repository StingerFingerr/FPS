using UnityEngine;

namespace Game_state_machine
{
    public class ExitState: IState
    {
        public void Enter()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public void Exit()
        {
            
        }
    }
}