using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Management
{
    public sealed class GameManager : MonoBehaviour
    {
        public delegate void GameStateChanged(GameState state);

        public static event GameStateChanged StateChanged;

        [SerializeField] private UnityEvent<GameState> onStateChanged;

        private GameState _currentState;
        public static GameManager Instance { get; private set; }

        public GameState CurrentState
        {
            set
            {
                if (value == _currentState) return;
                _currentState = value;
                StateChanged?.Invoke(_currentState);
                onStateChanged?.Invoke(_currentState);
            }
            get => _currentState;
        }

        private void Awake()
        {
            Instance = this;
            _currentState = GameState.Playing;
        }
    }
}
