using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class EnemyStateMachine
    {
        EnemyState CurrentState;

        private EnemyAIContext _context;
        public IEnemyActions Actions;

        public readonly IdleState _idleState;
        public readonly PatrolState _patrolState;
        public readonly AttackState _attackState;
        public readonly BombState _bombState;

        public EnemyStateMachine(EnemyAIContext context, IEnemyActions actions)
        {
            _context = context;
            Actions = actions;

            _idleState = new IdleState();
            _patrolState = new PatrolState(this, _context);
            _attackState = new AttackState(this, _context);
            _bombState = new BombState(this, _context);

            ChangeState(_patrolState);
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
            if (CurrentState is PatrolState)
            {
                if (_context.HasBomb)
                {
                    ChangeState(_bombState);
                    return;
                }
                if (_context.HasCharacter) ChangeState(_attackState);                
            }
            else if (CurrentState is AttackState)
            {
                if (_context.HasBomb)
                {
                    ChangeState(_bombState);
                    return;
                }
                if (!_context.HasCharacter) ChangeState(_patrolState);
            }
            else if (CurrentState is BombState)
            {
                if (!_context.HasBomb) ChangeState(_patrolState);
            }
        }
    }   
}