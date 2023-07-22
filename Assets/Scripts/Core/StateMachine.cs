using UnityEngine;

namespace Core
{
    public sealed class StateMachine<T> where T : MonoBehaviour
    {
        public State<T> CurrentState { get; private set; }

        public void Initialize(State<T> startingState)
        {
            CurrentState = startingState;
            CurrentState.OnStart();
        }

        public void ChangeState(State<T> state)
        {
            CurrentState.OnEnd();
            CurrentState = state;
            CurrentState.OnStart();
        }
    }
}
