using Attributes;
using Core;
using UnityEngine;

namespace Management
{
    public sealed class GameStateSetter : MonoBehaviour
    {
        [SerializeField] private GameState state;
        [SerializeField, ReadOnly] private GameState lastState;

        private void Start()
        {
            if (!GameManager.Instance)
                Debug.LogWarning("<b>GameManager</b> was not found in the scene.", this);
        }

        public void ResetLastGameState(){
            SetGameState(lastState);
        }
        
        public void SetGameState()
        {
            lastState = GameManager.Instance.CurrentState;
            GameManager.Instance.CurrentState = state;
        }
        public void SetGameState(GameState state)
        {
            lastState = GameManager.Instance.CurrentState;
            GameManager.Instance.CurrentState = state;
        }

    }
}
