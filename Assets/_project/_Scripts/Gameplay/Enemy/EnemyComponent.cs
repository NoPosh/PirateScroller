using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Enemy;
using TestGame.Core.Health;
using TestGame.Core.Interfaces;
using TestGame.Core.Movement;
using TestGame.Data.Settings;
using TestGame.Gameplay.Character;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace TestGame.Gameplay.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyComponent : MonoBehaviour, IDamageable, IForcable
    {
        [SerializeField] private CharacterSettings _enemySettings;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private EnemyDetector _enemyDetector;
        private JumpHandler _jumpHandler;

        //Нужна ли сборная STM через SO? Было бы удобно, но будто излишне для таких врагов
        private Core.Enemy.Enemy _enemy;
        private Rigidbody2D _rb;

        private bool IsGrounded => _groundCheck.IsGrounded;
        private bool CanJump => _jumpHandler.CanJump;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _jumpHandler = GetComponent<JumpHandler>();

            var mover = new PhysicalMover(_rb, _enemySettings._physicalMoveSettings);
            var health = new HealthSystem(_enemySettings.maxHealth);

            var stm = new EnemyStateMachine();

            _enemy = new Core.Enemy.Enemy(health, mover, stm);

            //_enemyDetector.OnCharacterEnter += v => _enemy.StateMachine.StateInfo.SetCharacterPosition(v);
            //_enemyDetector.OnCharacterExit += _enemy.StateMachine.StateInfo.ResetCharacterPos;
            //
            //_enemyDetector.OnBombEnter += bomb => _enemy.StateMachine.StateInfo.SetBombPosition(bomb.transform.position);
            //_enemyDetector.OnBombExit += _enemy.StateMachine.StateInfo.ResetBombPosition;
        }

        private void FixedUpdate()
        {
            _enemy.PhysicalMover.SetGrounded(IsGrounded);
            _enemy.StateMachine.StateInfo.NeedJump = CanJump;

            if (_enemyDetector.HasBombs)
                _enemy.StateMachine.StateInfo.SetBombPosition(_enemyDetector.GetNearestBomb().transform.position);
            else
                _enemy.StateMachine.StateInfo.ResetBombPosition();

            if (_enemyDetector.HasCharacter)
                _enemy.StateMachine.StateInfo.SetCharacterPosition(_enemyDetector.CharacterPosition);
            else
                _enemy.StateMachine.StateInfo.ResetCharacterPos();

            _enemy.FixedUpdate(Time.fixedDeltaTime);

        }

        public void TakeDamage(DamageInfo info)
        {
            _enemy.TakeDamage(info);
        }

        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            _enemy.AddForce(force, mode);
        }

    }
}