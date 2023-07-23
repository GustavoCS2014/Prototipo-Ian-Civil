using Entities.Boss.States;

namespace Entities.Boss
{
    public class BossController : BaseEntityController<BossController>
    {
        public BossSettings Settings => settings as BossSettings;

        public IdleState IdleState { get; private set; }
        public DashState DashState { get; private set; }
        public ShootState ShootState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            IdleState = new IdleState(this, StateMachine);
            DashState = new DashState(this, StateMachine);
            ShootState = new ShootState(this, StateMachine);
        }

        private void Start()
        {
            Initialize(IdleState);
        }
    }
}
