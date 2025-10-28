using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class EnemyAttackHandler
    {
        private int _attackDamage = 1;
        private float _attackCooldown = 2f;

        private float _attackTimner = 0f;

        public event Action OnAttack;

        public void Update(float fixedDeltaTime)
        {
            if (_attackTimner > 0f) _attackTimner -= fixedDeltaTime;
        }

        public bool AttackRequest(IDamageable damageable)
        {
            if (_attackTimner <= 0f)
            {
                damageable.TakeDamage(new DTO.DamageInfo(_attackDamage));
                _attackTimner = _attackCooldown;
                OnAttack?.Invoke();

                return true;
            }
            return false;
        }
    }
}