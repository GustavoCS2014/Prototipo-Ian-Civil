using Media;
using UnityEngine;

namespace Entities
{
    public abstract class BaseEntitySettings : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private LayerMask groundLayer;
        public LayerMask GroundLayer => groundLayer;

        [SerializeField] private float groundCheckRadius;
        public float GroundCheckRadius => groundCheckRadius;

        [Header("Media")]
        [SerializeField] private EntityAnimationsMedia animationsMedia;
        public float IdleAnimationTime => animationsMedia && animationsMedia.Idle ? animationsMedia.Idle.length : 0f;
        public float JumpAnimationTime => animationsMedia && animationsMedia.Jump ? animationsMedia.Jump.length : 0f;
        public float LandAnimationTime => animationsMedia && animationsMedia.Land ? animationsMedia.Land.length : 0f;
        public float DieAnimationTime => animationsMedia && animationsMedia.Die ? animationsMedia.Die.length : 0f;
    }
}
