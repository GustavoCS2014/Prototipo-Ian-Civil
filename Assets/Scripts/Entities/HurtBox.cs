using System;
using Core;
using UnityEngine;
using Utilities;

namespace Entities
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class HurtBox : MonoBehaviour, IDamageTaker
    {
        public event Action<int> DamageTaken;
        public event Action HealthDepleted;

        [SerializeField, Min(0)] private int health;
        public int Health
        {
            get => health;
            set => health = value.ClampMin(0);
        }

        [SerializeField, Min(0f)] private float damageTime;
        public float DamageTime => damageTime;

        public void TakeDamage(int damage)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, int.MaxValue);
            DamageTaken?.Invoke(damage);
            if (health <= 0)
                HealthDepleted?.Invoke();
        }
    }
}
