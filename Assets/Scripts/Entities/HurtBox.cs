using System;
using Attributes;
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

        [SerializeField, Min(1)] private int maxHealth;
        [SerializeField, ReadOnly] private int health;
        [SerializeField] private bool hasCooldown;
        [SerializeField]
        [ShowIfBool(nameof(hasCooldown))]
        [Min(0f)]
        private float damageTime;
        [SerializeField] private UnityEvent onHealthDepleted;

        private bool _isInvulnerable;
        private float _timer;

        public int MaxHealth => maxHealth;

        public int Health
        {
            get => health;
            set => health = value.ClampMin(0);
        }

        public float DamageTime => damageTime;

        public float ProgressNormalized => (float)Health / MaxHealth;
        public Range ProgressRange => new(0, MaxHealth);

        private void Awake()
        {
            Health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (_isInvulnerable) return;

            if (hasCooldown)
                _isInvulnerable = true;

            Health -= damage;
            DamageTaken?.Invoke(damage);
            ProgressUpdated?.Invoke(ProgressNormalized);

            if (health > 0) return;
            gameObject.SetActive(false);
            HealthDepleted?.Invoke();
            onHealthDepleted?.Invoke();
        }

        private void Update()
        {
            if (!hasCooldown) return;
            if (!_isInvulnerable) return;

            if (_timer >= damageTime)
            {
                _timer = 0;
                _isInvulnerable = false;
            }

            _timer += Time.deltaTime;
        }
    }
}
