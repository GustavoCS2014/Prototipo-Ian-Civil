using System;
using Core;
using UnityEngine;

namespace Management
{
    public class GameStateSetter : MonoBehaviour
    {
        [SerializeField] private GameState state;

        private void Start()
        {
            if (!GameManager.Instance)
                Debug.LogWarning("<b>GameManager</b> was not found in the scene.", this);
        }

        public void SetGameState()
        {
            GameManager.Instance.CurrentState = state;
        }
    }
}
