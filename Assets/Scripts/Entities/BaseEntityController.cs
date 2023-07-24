using Core;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseEntityController<T> : MonoBehaviour where T : BaseEntityController<T>
    {
        [SerializeField] protected BaseEntitySettings settings;

        private float _direction;

        protected StateMachine<T> StateMachine { get; private set; }

        public Rigidbody2D Rigidbody { get; private set;  }

        protected virtual void Awake()
        {
            StateMachine = new StateMachine<T>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

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
            settings.GroundCheckRadius,
            settings.GroundLayer
        );

        protected void Initialize(State<T> state) => StateMachine.Initialize(state);

        private void Update() => StateMachine.CurrentState.Update();

        private void FixedUpdate() => StateMachine.CurrentState.FixedUpdate();

        protected virtual void OnDestroy() => StateMachine.Kill();

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (settings)
                Gizmos.DrawWireSphere(transform.position, settings.GroundCheckRadius);
        }
    }
}
