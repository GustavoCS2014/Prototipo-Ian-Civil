using UnityEngine;

namespace Entities
{
    public abstract class BaseEntitySettings : ScriptableObject
    {
        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;

        [SerializeField] private LayerMask groundLayer;
        public LayerMask GroundLayer => groundLayer;

        [SerializeField] private float groundCheckRadius;
        public float GroundCheckRadius => groundCheckRadius;
    }
}
