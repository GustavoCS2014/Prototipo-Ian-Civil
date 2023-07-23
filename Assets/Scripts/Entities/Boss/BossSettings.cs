using UnityEngine;
using Utilities;

namespace Entities.Boss
{
    [CreateAssetMenu(fileName = "Boss Settings", menuName = "Bosses/Boss Settings")]
    public class BossSettings : BaseEntitySettings
    {
        [Header("Idle")]
        [SerializeField] private Range idleTime;
        public Range IdleTime => idleTime;

        [Header("Dash")]
        [SerializeField] private float dashSpeed;
        public float DashSpeed => dashSpeed;

        [SerializeField] private float dashDistance;
        public float DashDistance => dashDistance;

        public float DashTime => DashDistance / DashSpeed;

        [SerializeField] private AnimationCurve dashCurve;
        public float DashCurve(float t) => dashCurve.Evaluate(t);
    }
}
