using UnityEngine;

namespace Entities.Player
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "Player/Player Settings")]
    public sealed class PlayerSettings : BaseEntitySettings
    {
        [SerializeField] private float speed;
        public float Speed => speed;

        [SerializeField] private float jumpHeight;
        public float JumpHeight => jumpHeight;

        [SerializeField] private float originalGravityScale;
        public float OriginalGravityScale => originalGravityScale;

        [SerializeField, Range(0f, 1f)] private float airInputInfluence;
        public float AirInputInfluence => airInputInfluence;

        [SerializeField] private LayerMask collectableLayer;
        public LayerMask CollectableLayer => collectableLayer;

        [SerializeField] private LayerMask stairsMask;
        public LayerMask StairsMask => stairsMask;
    }
}
