using Core;
using UnityEngine;

namespace Management
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private GameState mouseVisibleOnStates;

        private void Awake()
        {
            GameManager.StateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.StateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            Cursor.visible = (state & mouseVisibleOnStates) != 0;
        }
    }
}
