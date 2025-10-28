using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Health;
using TestGame.Core.Interfaces;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class EnemyAI
    {
        private IEnemyActions _actions;
        private EnemyAIContext _context;

        private HealthSystem _health;
        private EnemyStateMachine _stateMachine;

        private bool _isAlive = true;
        public bool IsAlive => _isAlive;

        

        public EnemyAI(IEnemyActions actions, EnemyAIContext context, HealthSystem health)
        {
            _actions = actions;
            _context = context;
            _health = health;
            _stateMachine = new EnemyStateMachine(context, actions);

            _health.OnDead += Die;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {

        }

        public void Update(float deltaTime)
        {
            if (_isAlive)
                _stateMachine.Update(deltaTime);
        }

        public void TakeDamage(DamageInfo info)
        {
            _health.TakeDamage(info);
        }

        private void Die()
        {
            _isAlive = false;
            _actions.SetDirection(Vector2.zero);
        }
    }
}