using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class EnemyStateMachine
    {
        public EnemyStateInfo StateInfo;

        EnemyState CurrentState;

        public readonly IdleState _idleState;
        public readonly PatrolState _patrolState;

        public event Action<Vector2> OnMoveDirection;
        public event Action OnJumpRequest;

        public EnemyStateMachine()
        {
            _idleState = new IdleState();
            _patrolState = new PatrolState(this);

            ChangeState(_patrolState);

            StateInfo = new EnemyStateInfo();
        }

        public void ChangeState(EnemyState state)
        {
            if (CurrentState != null) CurrentState.Exit();

            if (CurrentState == state) return;

            CurrentState = state;
            CurrentState.Enter();
        }

        public void Update(float deltaTime)
        {
            CheckTransitions();

            if (CurrentState != null) CurrentState.Update(deltaTime);

        }

        private void CheckTransitions()
        {
            if (CurrentState == null) return;

            if (CurrentState is PatrolState)
            {
                //Если рядом игрок или бомба, то переход
            }
            else if (CurrentState is IdleState)
            {
                //Переходы
            }
            //Другие переходы
        }

        public void SetMoveDirection(Vector2 dir)
        {
            OnMoveDirection?.Invoke(dir);
        }

        public void JumpRequest()
        {
            OnJumpRequest?.Invoke();
        }
    }


    public struct EnemyStateInfo
    {
        public bool NeedJump;
        public Vector2 CurrentVelocity;

    }
}