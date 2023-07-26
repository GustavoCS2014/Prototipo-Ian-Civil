using Entities.Settings;
using UnityEngine;
using Utilities;

namespace Entities.Boss
{
    [CreateAssetMenu(fileName = "Boss Settings", menuName = "Bosses/Boss Settings")]
    public sealed class BossSettings : BaseEntitySettings
    {
        [Header("Idle")]
        [SerializeField] private Range idleTime;
        public float IdleTime => idleTime.Random;

        [Header("Dash")]
        [SerializeField] private float dashSpeed;
        public float DashSpeed => dashSpeed;

        [SerializeField] private float dashDistance;
        public float DashDistance => dashDistance;

        public float DashTime => DashDistance / DashSpeed;

        [SerializeField] private AnimationCurve dashCurve;
        public float DashCurve(float t) => dashCurve.Evaluate(t);

        [Header("Shoot")]
        [SerializeField] private float shootCooldownTime;
        public float ShootCooldownTime => shootCooldownTime;

        [SerializeField] private float timeBetweenShots;
        public float TimeBetweenShots => timeBetweenShots;

        [SerializeField] private int timesToShoot;
        public int TimesToShoot => timesToShoot;

        [Header("Jump")]
        [SerializeField] private Vector2 jumpDisplacement;
        public Vector2 JumpDisplacement => jumpDisplacement;
    }
}
