using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Health;
using TestGame.Core.Movement;
using UnityEngine;

namespace TestGame.Gameplay.Character
{
    public class Character
    {
        public HealthSystem Health { get; }
        public PhysicalMover PhysicalMover { get; }

        public Character(HealthSystem health, PhysicalMover mover)
        {
            Health = health;
            PhysicalMover = mover;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
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
    }
}