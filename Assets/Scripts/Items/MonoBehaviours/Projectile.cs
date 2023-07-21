using UnityEngine;

namespace Items.MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileInfo projectileInfo;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            Destroy(gameObject, projectileInfo.Lifetime);
        }

        public void Launch(Vector2 direction)
        {
            _rigidbody.velocity = direction * projectileInfo.Speed;
        }
    }
}
