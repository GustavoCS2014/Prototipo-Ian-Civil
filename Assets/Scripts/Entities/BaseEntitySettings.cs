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

        public float DieTime => 0.5f;
    }
}
