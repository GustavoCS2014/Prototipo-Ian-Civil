namespace Core
{
    public abstract class State
    {
        protected StateMachine StateMachine { get; }

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
