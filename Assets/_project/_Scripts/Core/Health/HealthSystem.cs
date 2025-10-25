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
            if (_currentHealth == 0) return;
            _currentHealth -= info.Value;
            Debug.Log($"Получен урон, осталось {_currentHealth} / {_maxHealth}");
            

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }

            OnDamaged?.Invoke();
        }

        public void Heal(int amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
        }

        private void Die()
        {
            Debug.Log("Персонаж умер");
            _currentHealth = 0;
            OnDead?.Invoke();
        }

    }
}