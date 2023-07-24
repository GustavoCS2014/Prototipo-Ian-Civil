using UnityEngine;

namespace Entities
{
    public abstract class BaseEntitySettings : ScriptableObject
    {
        [Header("Health")]
        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;

        [SerializeField] private float damageTime;
        public float DamageTime => damageTime;

        [Header("Movement")]
        [SerializeField] private LayerMask groundLayer;
        public LayerMask GroundLayer => groundLayer;

        [SerializeField] private float groundCheckRadius;
        public float GroundCheckRadius => groundCheckRadius;

        public float DieTime => 0.5f;
    }
}
