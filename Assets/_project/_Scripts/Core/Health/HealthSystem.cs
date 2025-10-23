using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.Health
{
    public class HealthSystem
    {
        private int _maxHealth;
        private int _currentHealth;

        public HealthSystem(int maxHealth)
        {
            _maxHealth = maxHealth;

            _currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;

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

        }

    }
}