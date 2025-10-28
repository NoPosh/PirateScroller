using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class AttackState : EnemyState
    {
        private EnemyStateMachine _stateMachine;
        private EnemyAIContext _context;

        private float _attackDistance = 0.75f;

        Vector2 directionToPlayer;

        public AttackState(EnemyStateMachine stateMachine, EnemyAIContext context)
        {
            _stateMachine = stateMachine;
            _context = context;
        }

        public override void Enter()
        {

        }

        public override void Update(float deltaTime)
        {
            UpdateMovementDirection();
            Vector2 charPosition = _context.CharacterPosition != null ? _context.CharacterPosition.Value : Vector2.zero;
            directionToPlayer = charPosition - (Vector2)_context.Transform.position;

            if (directionToPlayer.magnitude < _attackDistance)
            {
                //_stateMachine.Actions;
                _stateMachine.Actions.Attack(_context.CharacterDamageable);
                //Атака
            }
            //+ контроль прыжка
        }

        public override void Exit()
        {
            _stateMachine.Actions.SetDirection(Vector2.zero);
        }

        private void UpdateMovementDirection()
        {
            _stateMachine.Actions.SetDirection(directionToPlayer);
        }
    }
}