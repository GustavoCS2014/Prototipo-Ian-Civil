namespace Core
{
    public abstract class State
    {
        protected readonly StateMachine StateMachine;

        protected State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void OnStart() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void OnEnd() { }
    }
}
