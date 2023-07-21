using Core;
using Player.States;
using UnityEngine;

namespace Player
{
    public sealed class PlayerController : MonoBehaviour
    {
        private static StateMachine _stateMachine;

        #region Serialized Fields

        [SerializeField] private PlayerSettings settings;
        [SerializeField] private new Rigidbody2D rigidbody;

        public PlayerSettings Settings => settings;
        public Rigidbody2D Rigidbody => rigidbody;

        #endregion

        #region States

        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }
        public JumpState JumpState { get; private set; }

        #endregion

        #region Status Members

        public float Direction { get; set; }

        public bool Grounded => Physics2D.OverlapCircle(
            rigidbody.position,
            radius: 0.1f,
            settings.GroundLayer
        );

        #endregion

        private void Awake()
        {
            _stateMachine = new StateMachine();
            IdleState = new IdleState(this, _stateMachine);
            MoveState = new MoveState(this, _stateMachine);
            JumpState = new JumpState(this, _stateMachine);
        }

        private void Start() => _stateMachine.Initialize(IdleState);

        private void Update() => _stateMachine.CurrentState.Update();

        private void FixedUpdate() => _stateMachine.CurrentState.FixedUpdate();
    }
}
