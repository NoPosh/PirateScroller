using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.EventBus;
using TestGame.Core.Health;
using TestGame.Core.Input;
using TestGame.Core.Interfaces;
using TestGame.Core.Movement;
using TestGame.Core.Weapon;
using TestGame.Data.Settings;
using UnityEngine;

namespace TestGame.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterComponent : MonoBehaviour, IDamageable, IForcable
    {
        [SerializeField] private CharacterSettings _characterSettings;

        [Header("Прочее")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CharacterInteractor _interactor;

        private InteractionContext _interactionContext;
        private Character _character;
        private Rigidbody2D _rb;
        private InputHandler _inputHandler;
        [SerializeField] private GroundCheck _groundCheck;

        private bool IsGrounded => _groundCheck.IsGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            var mover = new PhysicalMover(_rb, _characterSettings._physicalMoveSettings);
            var health = new HealthSystem(_characterSettings.maxHealth);

            _inputHandler = new InputHandler();

            CombatSystem _combatSystem = new CombatSystem(_spawnPoint, _characterSettings._throwForce);
            _combatSystem.SetWeapon(_characterSettings.startBomb);
            _combatSystem.AddBomb(10);

            _character = new Character(health, mover, _combatSystem);

            CharacterActions actions = new CharacterActions(_character.AddBomb, _character.Heal);
            _interactionContext = new InteractionContext(gameObject, actions);
            _interactor.Init(_interactionContext);

            _inputHandler.OnMove += v => _character.PhysicalMover.SetDirection(v);
            _inputHandler.OnJump += _character.PhysicalMover.JumpRequest;
            _inputHandler.OnThrowBomb += v => _character.CombatSystem.ThrowBomb(v);
            _inputHandler.OnPutBomb += _character.CombatSystem.PutBomb;

            _character.Health.OnDamaged += () => _animator.SetTrigger("Hit");  
            _character.Health.OnDead += () => _animator.SetBool("IsDead", true);

            _character.Health.OnHealthChanged += ChangeHealth;

            _character.PhysicalMover.OnJump += () => _animator.SetTrigger("Jump");
        }

        private void FixedUpdate()
        {
            _character.PhysicalMover.SetGrounded(IsGrounded);
            _character.FixedUpdate(Time.fixedDeltaTime);

            if (_rb.velocity.x > 0.05f)
                _spriteRenderer.flipX = false;
            else if (_rb.velocity.x < -0.05f)
                _spriteRenderer.flipX = true;

            _animator.SetFloat("SpeedX", Mathf.Abs(_rb.velocity.x));
            _animator.SetFloat("SpeedY", _rb.velocity.y);
            _animator.SetBool("IsGrounded", IsGrounded);
        }

        private void Update()
        {
            _inputHandler.Update();
        }

        public void TakeDamage(DamageInfo info)
        {
            _character.TakeDamage(info);
        }

        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            _character.AddForce(force, mode);
        }

        private void ChangeHealth()
        {
            EventBus.Raise(new OnCharacterHealthChange(_character.Health.CurrentHealth));
        }
    }
}