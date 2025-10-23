using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Health;
using TestGame.Core.Input;
using TestGame.Core.Movement;
using UnityEngine;

namespace TestGame.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterComponent : MonoBehaviour
    {
        [Header("Настройки движения")]
        [SerializeField] private float moveForce;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float jumpForce;

        [Header("Настройки здоровья")]
        [SerializeField] private int maxHealth;

        private Character _character;
        private Rigidbody2D _rb;
        private InputHandler _inputHandler;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            var mover = new PhysicalMover(_rb, moveForce, maxSpeed, jumpForce);
            var health = new HealthSystem(maxHealth);

            _inputHandler = new InputHandler();

            _character = new Character(health, mover);

            _inputHandler.OnMove += v => _character.PhysicalMover.SetDirection(v);
        }

        private void FixedUpdate()
        {
            _character.FixedUpdate(Time.fixedDeltaTime);
        }

        private void Update()
        {
            _inputHandler.Update();
        }
    }
}