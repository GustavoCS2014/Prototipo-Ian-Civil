using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Management
{
    public sealed class GameManager : MonoBehaviour
    {
        public delegate void GameStateChanged(GameState state);

        public static event GameStateChanged StateChanged;

        [SerializeField] private GameState currentState;

        [SerializeField] private UnityEvent onSceneIntroState;
        [SerializeField] private UnityEvent onPlayingState;
        [SerializeField] private UnityEvent onPausedState;
        [SerializeField] private UnityEvent onSceneOutroState;

        public static GameManager Instance { get; private set; }

        public GameState CurrentState
        {
            set
            {
                if (value == currentState) return;
                currentState = value;
                StateChanged?.Invoke(currentState);
            }
            get => currentState;
        }

        private void Awake()
        {
            Instance = this;
            currentState = GameState.Playing;
            StateChanged += OnStateChanged;
        }

        private void OnStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.SceneIntro: onSceneIntroState?.Invoke(); break;
                case GameState.Playing: onPlayingState?.Invoke(); break;
                case GameState.Paused: onPausedState?.Invoke(); break;
                case GameState.OutroScene: onSceneOutroState?.Invoke(); break;
            }
        }

        private void OnDestroy()
        {
            StateChanged -= OnStateChanged;
        }
    }
}
