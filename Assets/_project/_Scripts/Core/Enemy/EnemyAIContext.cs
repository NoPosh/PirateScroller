using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using TestGame.Core.Weapon;
using UnityEngine;

namespace TestGame.Core.Enemy
{
    public class EnemyAIContext
    {
        public readonly Transform Transform;

        public bool IsGrounded;
        public bool IsInJumpPlace;

        public bool HasCharacter;
        public Vector2? CharacterPosition;
        public IDamageable CharacterDamageable;
        public IForcable CharacterForcable;

        public bool HasBomb;
        public Vector2? BombPosition;
        public BaseBomb Bomb;

        public Vector2 CurrentVelocity;

        public EnemyAIContext(Transform transform)
        {
            Transform = transform;
        }
        //Тут вся инфа, которую надо считывать (Например еще сюда игрока надо, ближайшую бомбу и тд)
    }
}