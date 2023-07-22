using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Management
{
    public class PauseManager : MonoBehaviour
    {
        public static event Action Paused;
        public static event Action Unpaused;

        [SerializeField] private UnityEvent onPause;
        [SerializeField] private UnityEvent onUnpause;

        private void Start()
        {
            GameInput.PausePerformed += OnPausePerformed;
        }

        private void OnPausePerformed(InputAction.CallbackContext context)
        {
            var gameManager = GameManager.Instance;

            gameManager.CurrentState = gameManager.CurrentState is GameState.Playing
                ? GameState.Paused
                : GameState.Playing;

            if (gameManager.CurrentState is GameState.Paused)
            {
                Time.timeScale = 0f;
                Paused?.Invoke();
                onPause?.Invoke();
            }
            else
            {
                Time.timeScale = 1f;
                Unpaused?.Invoke();
                onUnpause?.Invoke();
            }
        }
    }
}
