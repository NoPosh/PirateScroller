using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using TestGame.Data.Settings;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class EnemyAttackHandler
    {
        EnemyAttackSettings _settings;
        private float _attackTimner = 0f;

        public event Action OnAttack;

        public EnemyAttackHandler(EnemyAttackSettings settings)
        {
            _settings = settings;
        }

        public void Update(float fixedDeltaTime)
        {
            if (_attackTimner > 0f) _attackTimner -= fixedDeltaTime;
        }

        public bool AttackRequest(IDamageable damageable)
        {
            if (_attackTimner <= 0f)
            {
                damageable.TakeDamage(new DTO.DamageInfo(_settings._attackDamage));
                _attackTimner = _settings._attackCooldown;
                OnAttack?.Invoke();

                return true;
            }
            return false;
        }
    }  
}