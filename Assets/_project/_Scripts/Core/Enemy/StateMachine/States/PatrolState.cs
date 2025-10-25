using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class PatrolState : EnemyState
    {
        //Начинает идти сначала вправо, если упирается во что-то, то проверяет на триггер прыжка: либо прыгает, либо разворачивается
        private EnemyStateMachine _stateMachine;
        private bool _isRight = true;

        private float _stuckTimer = 0f;
        private const float STUCK_TIME_THRESHOLD = 0.5f;

        public event Action<Vector2> OnMoveDirection;
        public event Action OnJumpRequest;
        
        public PatrolState(EnemyStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            _isRight = true;
            _stuckTimer = 0f;
        }

        public override void Update(float deltaTime)
        {
            if (Mathf.Abs(_stateMachine.StateInfo.CurrentVelocity.x) < 0.1f)
            {
                _stuckTimer += deltaTime;

                if (_stuckTimer > STUCK_TIME_THRESHOLD)
                {
                    HandleObstacle();
                    _stuckTimer = 0f;
                }
            }
            else
            {
                _stuckTimer = 0f;
            }

            UpdateMovementDirection();
        }

        private void HandleObstacle()
        {
            if (_stateMachine.StateInfo.NeedJump)
            {
                _stateMachine.JumpRequest();
            }
            else
            {
                _isRight = !_isRight;
                UpdateMovementDirection();
            }
        }

        private void UpdateMovementDirection()
        {           
            Vector2 direction = _isRight ? Vector2.right : Vector2.left;
            _stateMachine.SetMoveDirection(direction);
        }

        public override void Exit()
        {
            OnMoveDirection?.Invoke(Vector2.zero);
        }
    }
}