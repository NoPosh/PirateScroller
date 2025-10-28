using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using TestGame.Core.Movement;
using TestGame.Core.Weapon;
using TestGame.Gameplay.Enemy;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class EnemyActions : IEnemyActions
    {
        private PhysicalMover _mover;
        private EnemyAttackHandler _attackHandler;
        private IBombInteraction _bombInteraction;
        public EnemyActions(PhysicalMover mover, EnemyAttackHandler attackHandler, IBombInteraction bombInteraction)
        {
            _mover = mover;
            _attackHandler = attackHandler;
            _bombInteraction = bombInteraction;
        }

        public void Update(float deltaTime)
        {
            _attackHandler.Update(deltaTime);
            _bombInteraction.Update(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            _mover.FixedUpdate(fixedDeltaTime);
        }

        public void SetDirection(Vector2 dir) => _mover.SetDirection(dir);

        public void Jump()
        {
            _mover.JumpRequest();
        }

        public void Attack(IDamageable damageable, IForcable forcable = null)
        {
            if (_attackHandler.AttackRequest(damageable))
            {
                //Добавить силу
            }
        }

        public void SetGrounded(bool isGrounded) => _mover.SetGrounded(isGrounded);

        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            _mover.AddForce(force, mode);
        }

        public void BombInteract(BaseBomb bomb)
        {
            _bombInteraction.InteractBomb(bomb);
        }
    }
}