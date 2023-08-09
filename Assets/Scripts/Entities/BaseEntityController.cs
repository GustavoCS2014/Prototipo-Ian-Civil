using Attributes;
using Core;
using UnityEngine;
using Utilities;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseEntityController<T> : MonoBehaviour where T : BaseEntityController<T>
    {
        [SerializeField, ReadOnly] private string currentState;
        [SerializeField] protected BaseEntitySettings settings;
        [SerializeField] protected HurtBox hurtBox;

        private float _direction;

        protected StateMachine<T> StateMachine { get; private set; }

        public Rigidbody2D Rigidbody { get; private set;  }

        protected virtual void Awake()
        {
            StateMachine = new StateMachine<T>();
            StateMachine.StateChanged += OnStateChanged;

            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public float Direction
        {
            get => _direction;
            set
            {
                Vector3 localScale = transform.localScale;

                value = value.Clamp(-1f, 1f);

                if (value != 0f)
                {
                    transform.localScale = new Vector3(
                        Mathf.Sign(value) * Mathf.Abs(localScale.x),
                        localScale.y,
                        localScale.z
                    );
                }

                _direction = value;
            }
        }

        public Vector2 FacingDirection => transform.localScale.x * Vector2.right;

        public bool Grounded => Physics2D.OverlapCircle(
            Rigidbody.position,
            settings.GroundCheckRadius,
            settings.GroundLayer
        );

        private void OnStateChanged(State<T> state) => currentState = state.ToString();

        private void Update() => StateMachine?.CurrentState?.Update();

        private void FixedUpdate() => StateMachine?.CurrentState?.FixedUpdate();

        protected virtual void OnDestroy()
        {
            if (StateMachine is null) return;
            StateMachine.Kill();
            StateMachine.StateChanged -= OnStateChanged;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (settings)
                Gizmos.DrawWireSphere(transform.position, settings.GroundCheckRadius);
        }

        #region Rigidbody

        public Vector2 Velocity
        {
            get => Rigidbody.velocity;
            set => Rigidbody.velocity = value;
        }

        public void MovePosition(Vector2 position) => Rigidbody.MovePosition(position);

        public void AddForce(Vector2 force, ForceMode2D mode) => Rigidbody.AddForce(force, mode);

        #endregion
    }
}
