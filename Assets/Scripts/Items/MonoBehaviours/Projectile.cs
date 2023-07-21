using System;
using Core;
using Items.ScriptableObjects;
using UnityEngine;

namespace Items.MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        public static event Action<Projectile, bool> OnHit;

        [SerializeField] private ProjectileInfo projectileInfo;
        [SerializeField] private bool destroyOnHit;

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

        private void FixedUpdate()
        {
            Transform t = transform;
            Collider2D other = Physics2D.OverlapCircle(t.position, t.localScale.magnitude, projectileInfo.TargetLayer);

            if (other && other != _lastCollider)
                _lastCollider = other;
            else
                return;

            if (!other.TryGetComponent(out IDamageTaker damageTaker)) return;

            bool success = damageTaker.TakeDamage(projectileInfo.Damage);
            OnHit?.Invoke(this, success);

            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
