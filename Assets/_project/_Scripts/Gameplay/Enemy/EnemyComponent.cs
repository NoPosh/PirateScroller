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
        }

        private void FixedUpdate()
        {
            _enemy.PhysicalMover.SetGrounded(IsGrounded);
            _enemy.StateMachine.StateInfo.NeedJump = CanJump;

            _enemy.FixedUpdate(Time.deltaTime);
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