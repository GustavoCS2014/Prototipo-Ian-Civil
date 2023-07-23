using Core;
using Entities.Boss.States;
using UnityEngine;

namespace Entities.Boss
{
    public class BossController : MonoBehaviour
    {
        private static StateMachine<BossController> _stateMachine;

        public IdleState IdleState { get; private set; }
        public DashState DashState { get; private set; }
        public ShootState ShootState { get; private set; }

        private void Awake()
        {
            _stateMachine = new StateMachine<BossController>();
            IdleState = new IdleState(this, _stateMachine);
            DashState = new DashState(this, _stateMachine);
            ShootState = new ShootState(this, _stateMachine);
        }

        private void Start() => _stateMachine.Initialize(IdleState);

        private void Update() => _stateMachine.CurrentState.Update();

        private void FixedUpdate() => _stateMachine.CurrentState.FixedUpdate();

        private void OnDestroy() => _stateMachine.Kill();
    }
}
