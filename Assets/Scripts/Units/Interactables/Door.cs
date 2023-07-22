using System.Linq;
using UnityEngine;

namespace Units.Interactables
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private WallButton[] buttons;
        [SerializeField] private float activeTime;

        private void Start()
        {
            foreach (WallButton button in buttons)
            {
                button.ActiveTime = activeTime;
                button.Activated += OnButtonActivated;
            }
        }

        private void OnButtonActivated()
        {
            if (buttons.Any(button => !button.Active)) return;

            foreach (WallButton button in buttons)
            {
                button.Activated -= OnButtonActivated;
                button.SetPermanent();
            }

            OpenDoor();
        }

        private void OpenDoor()
        {
            Destroy(gameObject);
        }
    }
}
