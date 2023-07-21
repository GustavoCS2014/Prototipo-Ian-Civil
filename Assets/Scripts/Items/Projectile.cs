using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Items/Projectile")]
    public sealed class Projectile : ScriptableObject
    {
        [SerializeField] private int damage;
        public int Damage => damage;

        [SerializeField] private float speed;

        [SerializeField] private float lifetime;

        [SerializeField] private LayerMask layerMask;

        public static void Spawn(Projectile projectile, Vector3 position, Quaternion rotation)
        {
            var instance = new GameObject("Projectile", typeof(Rigidbody2D), typeof(CircleCollider2D))
            {
                layer = projectile.layerMask,
                transform =
                {
                    position = position,
                    rotation = rotation
                }
            };

            var rigidbody = instance.GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0f;
            rigidbody.velocity = instance.transform.right * projectile.speed;

            var collider = instance.GetComponent<CircleCollider2D>();
            collider.isTrigger = true;
            collider.radius = 0.1f;

            Destroy(instance, projectile.lifetime);
        }
    }
}
