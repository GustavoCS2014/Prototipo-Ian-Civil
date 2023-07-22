using System.Collections;
using System.Linq;
using UnityEngine;

namespace Units.Interactables
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private bool freezeButtonsOnOpen;
        [SerializeField] private float activeTime;
        [SerializeField, Min(1E-5f)] private float displacementTime;
        [SerializeField] private WallButton[] buttons;

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
                if (freezeButtonsOnOpen)
                    button.SetPermanent();
            }

            OpenDoor();
        }

        private void OpenDoor()
        {
            StartCoroutine(DisplaceDoor());
        }

        private IEnumerator DisplaceDoor()
        {
            var initialPos = transform.position;
            var targetPos = transform.position + Vector3.up * transform.localScale.y;

            for (var t = 0f; t < displacementTime * 1.25f; t += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(initialPos, targetPos, Mathf.Clamp01(t / displacementTime));
                yield return null;
            }
        }
    }
}
