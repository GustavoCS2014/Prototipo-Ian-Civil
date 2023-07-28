using Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class CollisionTrigger : MonoBehaviour
    {
        [SerializeField] private Mode use;

        [SerializeField]
        [ShowIfEnum(nameof(use), (int)Mode.Tag)]
        [Tag]
        private string tagToCompare;

        [SerializeField]
        [ShowIfEnum(nameof(use), (int)Mode.Layer)]
        private LayerMask targetLayer;

        [SerializeField] private UnityEvent onTriggerEnter;
        [SerializeField] private UnityEvent onTriggerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (use is Mode.Tag)
            {
                if (other.CompareTag(tagToCompare))
                    onTriggerEnter?.Invoke();
            }
            else
            {
                if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
                    onTriggerEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (use is Mode.Tag)
            {
                if (other.CompareTag(tagToCompare))
                    onTriggerExit?.Invoke();
            }
            else
            {
                if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
                    onTriggerExit?.Invoke();
            }
        }

        private enum Mode { Tag, Layer }
    }
}
