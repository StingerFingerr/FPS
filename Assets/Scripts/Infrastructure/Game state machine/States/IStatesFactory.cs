namespace Game_state_machine
{
    public interface IStatesFactory
    {
        IState Create<TState>() where TState : class, IState;
    }   
}