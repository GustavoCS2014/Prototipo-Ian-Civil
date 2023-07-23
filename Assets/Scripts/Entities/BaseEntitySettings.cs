using UnityEngine;

namespace Entities
{
    public class BaseEntitySettings : ScriptableObject
    {
        [SerializeField] private LayerMask groundLayer;
        public LayerMask GroundLayer => groundLayer;

        [SerializeField] private float groundCheckRadius;
        public float GroundCheckRadius => groundCheckRadius;
    }
}
