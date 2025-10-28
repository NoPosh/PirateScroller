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
        public int CurrentHealth => _currentHealth;

        public event Action OnDamaged;
        public event Action OnDead;
        public event Action OnHealthChanged;

        public HealthSystem(int maxHealth)
        {
            _maxHealth = maxHealth;

            _currentHealth = maxHealth;

            OnHealthChanged?.Invoke();
        }

        public void TakeDamage(DamageInfo info)
        {
            if (_currentHealth == 0) return;
            _currentHealth -= info.Value;           

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }

            OnDamaged?.Invoke();
            OnHealthChanged?.Invoke();
        }

        public void Heal(int amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            OnHealthChanged?.Invoke();
        }

        private void Die()
        {
            Debug.Log("Персонаж умер");
            _currentHealth = 0;
            OnDead?.Invoke();
        }

    }
}