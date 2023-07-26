using System;
using Core;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Range = Utilities.Range;

namespace Entities
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class HurtBox : MonoBehaviour, IDamageTaker, IHasProgress
    {
        public event Action<float> ProgressUpdated;
        public event Action<int> DamageTaken;
        public event Action HealthDepleted;

        [SerializeField] private BaseEntitySettings entitySettings;
        [SerializeField, Min(0)] private int health;
        [SerializeField] private UnityEvent onHealthDepleted;

        public int Health
        {
            get => health;
            set => health = value.ClampMin(0);
        }

        public float DamageTime => entitySettings.DamageTime;

        public float ProgressNormalized => (float)Health / entitySettings.MaxHealth;
        public Range ProgressRange => new(0, entitySettings.MaxHealth);

        private void Start()
        {
            if (!entitySettings) return;
            Health = entitySettings.MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            DamageTaken?.Invoke(damage);
            ProgressUpdated?.Invoke(ProgressNormalized);

            if (health > 0) return;
            gameObject.SetActive(false);
            HealthDepleted?.Invoke();
            onHealthDepleted?.Invoke();
        }
    }
}
