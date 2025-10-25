using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Health;
using TestGame.Core.Movement;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class Enemy
    {
        public HealthSystem Health {  get; }
        public PhysicalMover PhysicalMover { get; }
        public EnemyStateMachine StateMachine { get; }

        //+ модуль на зрение

        public Enemy(HealthSystem health, PhysicalMover mover, EnemyStateMachine stm)
        {
            Health = health;
            PhysicalMover = mover;
            StateMachine = stm;

            StateMachine.OnMoveDirection += v => SetMovementDirection(v);
            StateMachine.OnJumpRequest += RequestJump;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            StateMachine.StateInfo.CurrentVelocity = PhysicalMover.GetVelocity();
            StateMachine.Update(fixedDeltaTime);

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

        private void SetMovementDirection(Vector2 dir)
        {
            PhysicalMover.SetDirection(dir);
        }

        private void RequestJump()
        {
            PhysicalMover.JumpRequest(); 
        }
    }
}