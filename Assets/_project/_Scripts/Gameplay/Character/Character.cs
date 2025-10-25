using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Health;
using TestGame.Core.Movement;
using TestGame.Core.Weapon;
using UnityEngine;

namespace TestGame.Gameplay.Character
{
    public class Character
    {
        public HealthSystem Health { get; }
        public PhysicalMover PhysicalMover { get; }

        public CombatSystem CombatSystem { get; }

        private bool _isAlive = true;

        public Character(HealthSystem health, PhysicalMover mover, CombatSystem combat)
        {
            Health = health;
            PhysicalMover = mover;
            CombatSystem = combat;

            Health.OnDead += KillCharacter;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            if (_isAlive)
                PhysicalMover.FixedUpdate(fixedDeltaTime);
        }

        public void TakeDamage(DamageInfo info)
        {
            Health.TakeDamage(info);
        }

        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            PhysicalMover.AddForce(force, mode);
        }

        private void KillCharacter()
        {
            _isAlive = false;
            //ט עה
        }
    }
}