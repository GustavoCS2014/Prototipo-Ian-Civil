using Core;
using Entities.Player.States;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        private static StateMachine _stateMachine;

        private float _direction;

        #region Serialized Fields

        [SerializeField] private PlayerSettings settings;

        public PlayerSettings Settings => settings;
        public Rigidbody2D Rigidbody { get; private set;  }

        #endregion

        #region States

        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }
        public JumpState JumpState { get; private set; }
        public FallState FallState { get; private set; }

        #endregion

        #region Status Members

        public float Direction
        {
            get => _direction;
            set
            {
                Vector3 localScale = transform.localScale;
                if (value != 0f)
                {
                    transform.localScale = new Vector3(
                        value * Mathf.Abs(localScale.x),
                        localScale.y,
                        localScale.z
                    );
                }

                _direction = Mathf.Clamp(value, -1f, 1f);
            }
        }

        public Vector2 FacingDirection => transform.localScale.x * Vector2.right;

        public bool Grounded => Physics2D.OverlapCircle(
            Rigidbody.position,
            radius: 0.1f,
            settings.GroundLayer
        );

        #endregion

        private void Awake()
        {
            Instance = this;
            Rigidbody = GetComponent<Rigidbody2D>();
            _stateMachine = new StateMachine();
            IdleState = new IdleState(this, _stateMachine);
            MoveState = new MoveState(this, _stateMachine);
            JumpState = new JumpState(this, _stateMachine);
            FallState = new FallState(this, _stateMachine);
        }

        private void Start() => _stateMachine.Initialize(IdleState);

        private void Update() => _stateMachine.CurrentState.Update();

        private void FixedUpdate() => _stateMachine.CurrentState.FixedUpdate();
    }
}
