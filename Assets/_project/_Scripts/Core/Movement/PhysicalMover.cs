using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestGame.Core.Movement
{
    public class PhysicalMover
    {
        private Rigidbody2D _rb;
        private float _moveForce;
        private float _maxSpeed;
        private float _jumpForce;

        private float _stopLerp = 0.15f;

        private Vector2 _currentDirection;
        private bool _isGrounded;
        private bool _needJump = false;
        
        public PhysicalMover(Rigidbody2D rb, float moveForce, float maxSpeed, float jumpForce)
        {
            _rb = rb;
            _moveForce = moveForce;
            _maxSpeed = maxSpeed;
            _jumpForce = jumpForce;
        }

        //TODO: разделить maxSpeed по X и Y
        public void FixedUpdate(float fixedDeltaTime)
        {
            if (_currentDirection.sqrMagnitude > 0.01f)
            {
                _rb.AddForce(_currentDirection.normalized * _moveForce);
            }
            else
            {
                Vector2 slowMove = new Vector2(0, _rb.velocity.y);
                _rb.velocity = Vector2.Lerp(_rb.velocity, slowMove, _stopLerp);

            }

            if (_rb.velocity.magnitude > _maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            }

            if (_needJump && _isGrounded)
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);                
            }

            _needJump = false;
        }

        public void SetDirection(Vector2 direction)
        {
            _currentDirection = direction;

        }

        public void JumpRequest()
        {

            _needJump = true; 
        }

        public void SetGrounded(bool grounded) => _isGrounded = grounded;
    }
}