using System.Collections.Generic;
using Media;
using UnityEngine;

namespace Entities
{
    public abstract class BaseEntitySettings : ScriptableObject
    {
        [Header("Movement")]
        [Tooltip("The layer that dictates what the player can consider ground and jump from.")]
        [SerializeField] private LayerMask groundLayer;
        public LayerMask GroundLayer => groundLayer;

        [Tooltip("The radius the player checks arround his feet to decide if it's grounded or not.")]
        [SerializeField] private float groundCheckRadius;
        public float GroundCheckRadius => groundCheckRadius;

        [Tooltip("The material used to slide arround the terrain.")]
        [SerializeField] private PhysicsMaterial2D slipperyMaterial;
        public PhysicsMaterial2D SlipperyMaterial => slipperyMaterial;

        [Tooltip("The material used to not slide while on stairs.")]
        [SerializeField] private PhysicsMaterial2D gripMaterial;
        public PhysicsMaterial2D GripMaterial => gripMaterial;

        [Tooltip("The amount of time the player will be able to jump after leaving the ground.")]
        [SerializeField] private float coyoteTime;
        public float CoyoteTime => coyoteTime;

        [Space(10)]
        [Header("Media")]
        [SerializeField] private EntityAnimationsMedia animationsMedia;
        public float IdleAnimationTime => animationsMedia && animationsMedia.Idle ? animationsMedia.Idle.length : 0f;
        public float JumpAnimationTime => animationsMedia && animationsMedia.Jump ? animationsMedia.Jump.length : 0f;
        public float LandAnimationTime => animationsMedia && animationsMedia.Land ? animationsMedia.Land.length : 0f;
        public float DieAnimationTime => animationsMedia && animationsMedia.Die ? animationsMedia.Die.length : 0f;
    }
}
