using System;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PromptUI : MonoBehaviour
    {
        [SerializeField] private GameInputCommand inputCommand;
        [SerializeField] private string message;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private InputActionAsset actionsAsset;

        private void Awake()
        {
            GameInput.ControlSchemeChanged += OnControlSchemeChanged;
        }

        private void Start()
        {
            UpdateText();
        }

        private void OnDestroy()
        {
            GameInput.ControlSchemeChanged -= OnControlSchemeChanged;
        }

        private void OnControlSchemeChanged(ControlScheme controlScheme)
        {
            UpdateText();
        }

        private void UpdateText(ControlScheme controlScheme = ControlScheme.Keyboard)
        {
            int index = (int)controlScheme;
            InputBinding inputBinding = actionsAsset.FindAction(inputCommand.ToString()).bindings[index];
            text.text = string.Format(message, inputBinding.ToDisplayString());
        }
    }
}
