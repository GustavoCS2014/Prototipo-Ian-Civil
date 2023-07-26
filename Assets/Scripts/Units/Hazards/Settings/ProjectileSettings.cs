using UnityEngine;

namespace Units.Hazards.Settings
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Items/Projectile")]
    public sealed class ProjectileSettings : ScriptableObject
    {
        [SerializeField] private int damage;
        public int Damage => damage;

        [SerializeField] private float speed;
        public float Speed => speed;

        [SerializeField] private float lifetime;
        public float Lifetime => lifetime;

        [SerializeField] private LayerMask targetLayer;
        public LayerMask TargetLayer => targetLayer;
    }
}
