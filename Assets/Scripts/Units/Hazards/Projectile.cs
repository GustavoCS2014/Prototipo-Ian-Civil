using System;
using Core;
using Units.Hazards.Settings;
using UnityEngine;

namespace Units.Hazards
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Projectile : MonoBehaviour
    {
        public static event Action<Projectile, GameObject> Hit;

        [SerializeField] private float radius;
        [SerializeField] private bool destroyOnHit;
        [SerializeField] private ProjectileSettings settings;

        private Rigidbody2D _rigidbody;
        private Collider2D _lastCollider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            Destroy(gameObject, settings.Lifetime);
        }

        public void Launch(Vector2 direction)
        {
            _rigidbody.velocity = direction * settings.Speed;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void FixedUpdate()
        {
            Transform t = transform;
            Collider2D other = Physics2D.OverlapCircle(t.position, radius, settings.TargetLayer);

            if (other == _lastCollider) return;

            _lastCollider = other;

            if (!other) return;

            if (other.TryGetComponent(out IDamageTaker damageTaker))
                damageTaker.TakeDamage(settings.Damage);

            Hit?.Invoke(this, other.gameObject);

            if (destroyOnHit)
                Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Transform t = transform;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(t.position, radius);
        }
    }
}
