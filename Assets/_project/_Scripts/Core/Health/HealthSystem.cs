using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using UnityEngine;
using System;

namespace TestGame.Core.Health
{
    public class HealthSystem
    {
        private int _maxHealth;
        private int _currentHealth;

        public event Action OnDamaged;
        public event Action OnDead;

        public HealthSystem(int maxHealth)
        {
            _maxHealth = maxHealth;

            _currentHealth = maxHealth;
        }

        public void TakeDamage(DamageInfo info)
        {
            _currentHealth -= info.Value;

            OnDamaged?.Invoke();

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }
        }

        public void Heal(int amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
        }

        private void Die()
        {
            OnDamaged?.Invoke();
        }

    }
}