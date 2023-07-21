using System;
using Core;
using Player.States;
using UnityEngine;

namespace Player
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings settings;

        private static StateMachine _stateMachine;
        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }

        private void Awake()
        {
            _stateMachine = new StateMachine();
            IdleState = new IdleState(this, _stateMachine);
            MoveState = new MoveState(this, _stateMachine);
        }

        private void Start() => _stateMachine.Initialize(IdleState);

        private void Update() => _stateMachine.CurrentState.Update();

        private void FixedUpdate() => _stateMachine.CurrentState.FixedUpdate();
    }
}
