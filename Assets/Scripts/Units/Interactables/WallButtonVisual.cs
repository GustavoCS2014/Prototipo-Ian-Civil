using UnityEngine;

namespace Units.Interactables
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WallButtonVisual : MonoBehaviour
    {
        [SerializeField] private Color offColor;
        [SerializeField] private Color onColor;

        [SerializeField] private WallButton button;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            button.Activated += OnButtonActivated;
            button.Deactivated += OnButtonDeactivated;

            OnButtonDeactivated();
        }

        private void OnButtonActivated()
        {
            _spriteRenderer.color = onColor;
        }

        private void OnButtonDeactivated()
        {
            _spriteRenderer.color = offColor;
        }
    }
}
