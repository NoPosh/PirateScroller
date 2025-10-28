using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using TestGame.Core.Weapon;
using TestGame.Gameplay.Character;
using TestGame.Gameplay.Enemy;
using UnityEngine;

namespace TestGame.Gameplay.Enemy
{
    public class EnemySensors : MonoBehaviour
    {
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private JumpHandler _jumpHandler;
        [SerializeField] private EnemyCharacterDetector _enemyCharacterDetector;
        [SerializeField] private EnemyBombDetector _enemyBombDetector;

        public event Action OnCharacterEnter;
        public event Action OnCharacterExit;

        public bool IsGrounded => _groundCheck.IsGrounded;


        public bool HasCharacter => _enemyCharacterDetector.HasCharacter;
        public Vector2? CharacterPosition => _enemyCharacterDetector.CharacterPosition;
        public IDamageable CurrentCharacterDamageable => _enemyCharacterDetector.CurrentCharacterDamageable;
        public IForcable CharacterForcable => _enemyCharacterDetector.CharacterForcable;


        public bool HasBomb => _enemyBombDetector.HasBombs;
        public Vector2? BombPosition => _enemyBombDetector.FirstBombPosition;
        public BaseBomb CurrentBomb => _enemyBombDetector.FirstBomb;

        private void Awake()
        {
            _enemyCharacterDetector.OnCharacterEnter += () => OnCharacterEnter?.Invoke();
            _enemyCharacterDetector.OnCharacterExit += () => OnCharacterExit?.Invoke();
        }
        //+ JumpHandler запустить

        //Тут все, что считывает инфу из мира

        
    }
}