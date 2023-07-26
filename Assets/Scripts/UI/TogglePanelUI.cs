using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class TogglePanelUI : MonoBehaviour
    {
        [SerializeField] private bool startHidden;
        [SerializeField] private GameInputCommand showOnInput;
        [SerializeField, Min(0f)] private float showForSeconds;

        private void Start()
        {
            if (startHidden)
                Hide();
        }

        private void Awake()
        {
            if (showOnInput.HasFlag(GameInputCommand.Pause))
                GameInput.PausePerformed += OnInputPerformed;

            if (showOnInput.HasFlag(GameInputCommand.Skip))
                GameInput.SkipPressed += OnInputPerformed;
        }

        private void OnDestroy()
        {
            if (showOnInput.HasFlag(GameInputCommand.Pause))
                GameInput.PausePerformed -= OnInputPerformed;

            if (showOnInput.HasFlag(GameInputCommand.Skip))
                GameInput.SkipPressed -= OnInputPerformed;
        }

        private void OnInputPerformed(InputAction.CallbackContext callbackContext)
        {
            Show();
            if (showForSeconds > 0f)
                Invoke(nameof(Hide), showForSeconds);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
