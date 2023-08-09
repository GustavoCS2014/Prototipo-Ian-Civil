using System.Collections.Generic;
using Attributes;
using UnityEngine;

namespace Units.Hazards
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Items/Projectile")]
    public sealed class ProjectileInfo : ScriptableObject
    {
        [SerializeField] private int damage;
        public int Damage => damage;

        [SerializeField] private float speed;
        public float Speed => speed;

        [SerializeField] private float lifetime;
        public float Lifetime => lifetime;

        [SerializeField, Tag] private string[] targetTags;
        public IEnumerable<string> TargetTags => targetTags;
    }
}
