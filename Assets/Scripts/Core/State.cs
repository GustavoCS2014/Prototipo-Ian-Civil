namespace Core
{
    public abstract class State
    {
        protected readonly StateMachine StateMachine;

        protected State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public virtual void OnStart() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        // ReSharper disable Unity.PerformanceAnalysis
        public virtual void OnEnd() { }
    }
}
