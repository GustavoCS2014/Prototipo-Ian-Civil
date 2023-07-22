using System.Linq;
using UnityEngine;

namespace Units.Interactables
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private WallButton[] buttons;

        private void Start()
        {
            foreach (WallButton button in buttons)
                button.Activated += OnButtonActivated;
        }

        private void OnButtonActivated()
        {
            if (buttons.Any(button => !button.Active)) return;

            foreach (WallButton button in buttons)
            {
                button.Deactivated -= OnButtonActivated;
                button.SetPermanent();
            }

            Destroy(gameObject);
        }
    }
}
