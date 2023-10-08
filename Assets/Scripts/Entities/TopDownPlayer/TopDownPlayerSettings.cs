using UnityEngine;

namespace Entities.TopDownPlayer
{
    [CreateAssetMenu(fileName = "Top Down Player Settings", menuName = "Player/Top Down Settings")]
    public sealed class TopDownPlayerSettings : ScriptableObject
    {
        [SerializeField] private float speed;
        public float Speed => speed;

        [SerializeField] private float radius;
        public float Radius => radius;

        [SerializeField] private LayerMask wallLayer;
        public LayerMask WallLayer => wallLayer;
    }
}
