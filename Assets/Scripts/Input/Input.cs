using System;
using Core;
using Management;
using UnityEngine;

namespace Input
{
    public abstract class Input : MonoBehaviour
    {
        [SerializeField] protected GameState enabledInStates;

        private void Start()
        {
            GameManager.StateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.StateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            enabled = (enabledInStates & state) == state;
        }
    }
}
