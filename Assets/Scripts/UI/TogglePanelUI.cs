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

        private float _timer;

        private void Start()
        {
            if (startHidden)
                Hide();
        }

        private void Awake()
        {
            if (showOnInput.HasFlag(GameInputCommand.Pause))
                PauseInput.PausePerformed += OnInputPerformed;

            if (showOnInput.HasFlag(GameInputCommand.Skip))
                UIInput.SkipPressed += OnInputPerformed;
        }

        private void OnDestroy()
        {
            if (showOnInput.HasFlag(GameInputCommand.Pause))
                PauseInput.PausePerformed -= OnInputPerformed;

            if (showOnInput.HasFlag(GameInputCommand.Skip))
                UIInput.SkipPressed -= OnInputPerformed;
        }

        private void OnInputPerformed(InputAction.CallbackContext callbackContext)
        {
            _timer = 0f;
            Show();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (showForSeconds <= 0f)
                return;

            if (!gameObject.activeSelf) return;

            if (_timer >= showForSeconds)
            {
                _timer = 0f;
                Hide();
            }

            _timer += Time.unscaledDeltaTime;
        }
    }
}
