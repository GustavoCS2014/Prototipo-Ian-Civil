using Core;
using Management;
using UnityEngine;
using UnityEngine.Events;

namespace Cinematics
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private GameState triggerOnState;

        [SerializeField] private UnityEvent onStartCutscene;
        [SerializeField] private UnityEvent onEndCutscene;

        private void Start()
        {
            GameManager.StateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(GameState state)
        {
            if (state != triggerOnState) return;
            onStartCutscene?.Invoke();
            Invoke(nameof(EndCinematic), duration);
        }

        private void EndCinematic()
        {
            onEndCutscene?.Invoke();
        }
    }
}
