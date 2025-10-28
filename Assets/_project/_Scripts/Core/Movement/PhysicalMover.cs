using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Data.Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestGame.Core.Movement
{
    public class PhysicalMover
    {
        private PhysicalMoveSettings _settings;
        private Rigidbody2D _rb;

        private Vector2 _currentDirection;
        private bool _isGrounded;
        private bool _needJump = false;

        private Vector2 _playerVelocity;

        public event Action OnJump;

        public PhysicalMover(Rigidbody2D rb, PhysicalMoveSettings settings)
        {
            _rb = rb;
            _settings = settings;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            
            if (_currentDirection.sqrMagnitude > 0.01f)
            {
                _rb.AddForce(_currentDirection.normalized * _settings.MoveForce);
            }
            else
            {
                Vector2 slowMove = new Vector2(0, _rb.velocity.y);
                _rb.velocity = Vector2.Lerp(_rb.velocity, slowMove, _settings.StopLerp);

            }

            if (_rb.velocity.magnitude > _settings.MaxMoveSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _settings.MaxMoveSpeed;
            }
            
            if (_needJump && _isGrounded)
            {
                _rb.AddForce(Vector2.up * _settings.JumpForce, ForceMode2D.Impulse);  
                OnJump?.Invoke();
            }

            _needJump = false;
        }

        public void SetDirection(Vector2 direction)
        {
            _currentDirection = new Vector2(direction.x, 0);
        }

        public void JumpRequest()
        {
            _needJump = true; 
        }

        public void SetGrounded(bool grounded) => _isGrounded = grounded;

        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            _rb.AddForce(force, mode);
        }

        public Vector2 GetVelocity()
        {
            return _rb.velocity;
        }
    }
}