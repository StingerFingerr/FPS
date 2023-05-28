using Game_state_machine;
using UnityEngine;
using Zenject;

namespace Game_runner
{
    public class GameRunner: MonoBehaviour
    {
        private IGameStateMachine _stateMachine;
        
        [Inject]
        private void Construct(IGameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        private void Start() => 
            _stateMachine.Enter<InitialState>();
    }
}