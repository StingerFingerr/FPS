using Zenject;

namespace Game_state_machine
{
    public class StatesFactory: IStatesFactory
    {
        private readonly DiContainer _diContainer;
        
        public StatesFactory (DiContainer diContainer) => 
            _diContainer = diContainer;

        public IState Create<TState>() where TState : class, IState =>
            _diContainer.Resolve<TState>();
    }
}