using Core;
using Entities.Boss.States;
using Entities.Player;
using Management;
using UnityEngine;

namespace Entities.Boss
{
    public sealed class BossController : BaseEntityController<BossController>
    {
        [SerializeField] private Shooter shooter;
        [SerializeField] private HurtBox hurtBox;

        private PlayerController _player;

        public BossSettings Settings => settings as BossSettings;
        public Shooter Shooter => shooter;

        public IdleState IdleState { get; private set; }
        public DashState DashState { get; private set; }
        public ShootState ShootState { get; private set; }
        public JumpState JumpState { get; private set; }
        public FallState FallState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            IdleState = new IdleState(this, StateMachine);
            DashState = new DashState(this, StateMachine);
            ShootState = new ShootState(this, StateMachine);
            JumpState = new JumpState(this, StateMachine);
            FallState = new FallState(this, StateMachine);
        }

        private void Start()
        {
            _player = PlayerController.Instance;
            Direction = -1f;
            Initialize(IdleState);

            if (hurtBox)
            {
                hurtBox.Health = Settings.MaxHealth;
                hurtBox.HealthDepleted += OnHealthDepleted;
            }
        }

        private void OnDestroy()
        {
            if (hurtBox) hurtBox.HealthDepleted -= OnHealthDepleted;
        }

        private void OnHealthDepleted()
        {
            hurtBox.gameObject.SetActive(false);
            StopAllCoroutines();
            StateMachine.Kill();
            if (GameManager.Instance)
                GameManager.Instance.CurrentState = GameState.BossKilled;
        }

        public void FacePlayer()
        {
            Direction = _player.transform.position.x > transform.position.x ? 1f : -1f;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            if (!Settings) return;

            Gizmos.color = Color.cyan;

            Vector3 origin = transform.position + Vector3.up;
            Gizmos.DrawRay(origin, Settings.DashDistance * Vector3.left);
            Vector3 ray = new Vector3
            {
                x = -Settings.JumpDisplacement.x,
                y = Settings.JumpDisplacement.y
            };
            Gizmos.DrawRay(origin, ray);
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(0f, 0f, Screen.width, Screen.height),
                StateMachine.CurrentState.ToString(),
                new GUIStyle
                {
                    fontSize = 16,
                    alignment = TextAnchor.LowerLeft,
                    normal = { textColor = Color.white }
                }
            );
        }
    }
}
