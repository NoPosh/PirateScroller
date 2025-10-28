using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Enemy;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class BombState : EnemyState
    {
        private EnemyStateMachine _stateMachine;
        private EnemyAIContext _context;

        private float _attackDistance = 0.75f;

        Vector2 directionToBomb;

        public BombState(EnemyStateMachine stateMachine, EnemyAIContext context)
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

            Vector2 bombPosition = _context.BombPosition != null ? _context.BombPosition.Value : Vector2.zero;
            directionToBomb = bombPosition - (Vector2)_context.Transform.position;

            if (directionToBomb.magnitude < _attackDistance)
            {
                _stateMachine.Actions.BombInteract(_context.Bomb);
            }
            //+ контроль прыжка
        }

        public override void Exit()
        {
            _stateMachine.Actions.SetDirection(Vector2.zero);
        }

        private void UpdateMovementDirection()
        {
            _stateMachine.Actions.SetDirection(directionToBomb);
        }
    }
}