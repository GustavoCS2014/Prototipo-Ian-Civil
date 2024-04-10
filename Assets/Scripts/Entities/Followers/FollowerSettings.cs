using UnityEditor.Rendering;
using UnityEngine;

namespace Entities.Player
{
    [CreateAssetMenu(fileName = "Follower Settings", menuName = "Follower/Follower Settings")]
    public sealed class FollowerSettings : BaseEntitySettings
    {
        [SerializeField] private float idleArea;
        public float IdleArea => idleArea;
        [SerializeField] private float walkingArea;
        public float WalkingArea => walkingArea;
        [SerializeField] private float runningArea;
        public float RunningArea => runningArea;

        [SerializeField] private float walkingSpeed;
        public float WalkingSpeed => walkingSpeed;

        [SerializeField] private float runningSpeed;
        public float RunningSpeed => runningSpeed;

        [SerializeField] private float jumpHeight;
        public float JumpHeight => jumpHeight;

        [SerializeField] private float originalGravityScale;
        public float OriginalGravityScale => originalGravityScale;

        [SerializeField, Range(0f, 1f)] private float airInputInfluence;
        public float AirInputInfluence => airInputInfluence;

        [SerializeField] private LayerMask stairsMask;
        public LayerMask StairsMask => stairsMask;
    }
}
