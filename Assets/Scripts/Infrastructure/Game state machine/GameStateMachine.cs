using System;
using System.Collections.Generic;

namespace Game_state_machine
{
    public class GameStateMachine: IGameStateMachine
    {
        private IStatesFactory _statesFactory;
        private Dictionary<Type, IState> _states;
        private IState _currentState;
        
        public GameStateMachine (IStatesFactory statesFactory)
        {
            _statesFactory = statesFactory;
            _states = new();
        }
        
        
        public void Enter<TState>() where TState : class, IState
        {
            _currentState?.Exit();

            _currentState = GetState<TState>();
            
            _currentState.Enter();
        }

        private IState GetState<TState>() where TState: class, IState
        {
            if (_states.ContainsKey(typeof(TState)))
                return _states[typeof(TState)];
            else
                return CreateState<TState>();
        }

        private IState CreateState<TState>() where TState: class, IState
        {
            IState state = _statesFactory.Create<TState>();
            _states.Add(typeof(TState), state);
            return state;
        }
    }
}