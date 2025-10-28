using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Weapon;
using UnityEngine;

namespace TestGame.Core.Interfaces
{
    public interface IEnemyActions
    {
        void SetDirection(Vector2 dir);
        void Jump();
        void SetGrounded(bool isGrounded);
        public void AddForce(Vector2 force, ForceMode2D mode);

        void FixedUpdate(float fixedDeltaTime);

        void Update(float deltaTime);

        void Attack(IDamageable damageable, IForcable forcable = null);

        void BombInteract(BaseBomb bomb);

    }
}