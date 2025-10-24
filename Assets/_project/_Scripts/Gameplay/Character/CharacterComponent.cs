using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Health;
using TestGame.Core.Input;
using TestGame.Core.Interfaces;
using TestGame.Core.Movement;
using TestGame.Data.Settings;
using UnityEngine;

namespace TestGame.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterComponent : MonoBehaviour, IDamageable, IForcable
    {
        [Header("Настройки движения")]
        [SerializeField] private PhysicalMoveSettings _physicalMoveSettings;

        [Header("Настройки здоровья")]
        [SerializeField] private int maxHealth;

        private Character _character;
        private Rigidbody2D _rb;
        private InputHandler _inputHandler;
        [SerializeField] private GroundCheck _groundCheck;

        private bool IsGrounded => _groundCheck.IsGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            var mover = new PhysicalMover(_rb, _physicalMoveSettings);
            var health = new HealthSystem(maxHealth);

            _inputHandler = new InputHandler();

            _character = new Character(health, mover);

            _inputHandler.OnMove += v => _character.PhysicalMover.SetDirection(v);
            _inputHandler.OnJump += _character.PhysicalMover.JumpRequest;
        }

        private void FixedUpdate()
        {
            _character.PhysicalMover.SetGrounded(IsGrounded);
            _character.FixedUpdate(Time.fixedDeltaTime);
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
    }
}