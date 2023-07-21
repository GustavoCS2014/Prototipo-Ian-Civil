namespace Core
{
    public sealed class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            CurrentState.OnStart();
        }

        public void ChangeState(State state)
        {
            CurrentState.OnEnd();
            CurrentState = state;
            CurrentState.OnStart();
        }
    }
}
