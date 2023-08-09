using System;
using System.Linq;
using Core;
using UnityEngine;

namespace Units.Hazards
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Projectile : MonoBehaviour
    {
        public static event Action<Projectile, GameObject> Hit;

        [SerializeField] private bool destroyOnHit;
        [SerializeField] private ProjectileInfo projectileInfo;

        private Rigidbody2D _rigidbody;
        private Collider2D _lastCollider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            Destroy(gameObject, projectileInfo.Lifetime);
        }

        public void Launch(Vector2 direction)
        {
            _rigidbody.velocity = direction * projectileInfo.Speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!projectileInfo.TargetTags.Any(other.CompareTag))
                return;

            if (other == _lastCollider) return;

            _lastCollider = other;

            if (!other) return;

            if (other.TryGetComponent(out IDamageTaker damageTaker))
                damageTaker.TakeDamage(projectileInfo.Damage);

            Hit?.Invoke(this, other.gameObject);

            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
