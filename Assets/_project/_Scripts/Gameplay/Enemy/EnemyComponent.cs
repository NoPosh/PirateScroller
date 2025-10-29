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
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private EnemySensors _enemySensors;
        [SerializeField] private Animator _animator;    //Надо подключить аниматор, как лучше бы это сделать?
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private EnemyAttackSettings _attackSettings;

        private EnemyAI _enemyAI;
        private EnemyAIContext _context;
        private IEnemyActions _actions;

        private PhysicalMover _mover;
        private JumpHandler _jump;
        private IBombInteraction _bombIntreactions;
        private EnemyAttackHandler _attack;

        private HealthSystem _healthSystem;
        
        private Rigidbody2D _rb;

        private bool _isRight;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            _mover = new PhysicalMover(_rb, _enemySettings._physicalMoveSettings);
            _attack = new EnemyAttackHandler(_attackSettings);
            _bombIntreactions = new KickBombInteraction(transform);
            _actions = new EnemyActions(_mover, _attack, _bombIntreactions);

            _context = new EnemyAIContext(transform);
            //Еще надо инициализировать все остальное

            _healthSystem = new HealthSystem(_enemySettings.maxHealth);    //Это надо сделать отдельной системой            

            _enemyAI = new EnemyAI(_actions, _context, _healthSystem);

            _enemySensors.OnCharacterEnter += UpdateCharacterContext;
            _enemySensors.OnCharacterExit += UpdateCharacterContext;

            _healthSystem.OnDamaged += () => _animator.SetTrigger("Hit");
            _mover.OnJump += () => _animator.SetTrigger("Jump");

            _bombIntreactions.OnBombInteraction += () => _animator.SetTrigger("Attack");
            _attack.OnAttack += () => _animator.SetTrigger("Attack");

        }

        private void FixedUpdate()
        {       
            _actions.FixedUpdate(Time.fixedDeltaTime);
            _actions.SetGrounded(_enemySensors.IsGrounded);
            _context.IsGrounded = _enemySensors.IsGrounded;
        }

        private void Update()
        {
            _actions.Update(Time.deltaTime);

            if (_enemySensors.HasCharacter)
            {
                _context.CharacterPosition = _enemySensors.CharacterPosition;
            }
            if (_enemySensors.HasBomb)
            {
                _context.HasBomb = _enemySensors.HasBomb;
                _context.BombPosition = _enemySensors.BombPosition;
                _context.Bomb = _enemySensors.CurrentBomb;  //TODO: Вообще можно не бомбу, а интерфейс с доступными действиями бомбы
            }
            else
            {
                _context.HasBomb = _enemySensors.HasBomb;
            }

            _enemyAI.Update(Time.deltaTime);
            _context.CurrentVelocity = _rb.velocity;

            if (_context.CurrentVelocity.x < 0 && _isRight)
            {
                _spriteRenderer.flipX = true;
                _isRight = false;
            } 
            else if (_context.CurrentVelocity.x > 0 && !_isRight)
            {
                _spriteRenderer.flipX = false;
                _isRight = true;
            }

            _animator.SetFloat("SpeedX", Mathf.Abs(_context.CurrentVelocity.x));
            _animator.SetFloat("SpeedY", _context.CurrentVelocity.y);
           
            _animator.SetBool("IsGrounded", _context.IsGrounded);

            _animator.SetBool("IsDead", !_enemyAI.IsAlive);
        }

        public void TakeDamage(DamageInfo info)
        {
            _enemyAI.TakeDamage(info);
        }

        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            _actions.AddForce(force, mode);
        }

        private void UpdateCharacterContext()
        {
            _context.HasCharacter =_enemySensors.HasCharacter;
            _context.CharacterDamageable = _enemySensors.CurrentCharacterDamageable;
            _context.CharacterForcable = _enemySensors.CharacterForcable;
            _context.CharacterPosition = _enemySensors.CharacterPosition;
        }

    }
}