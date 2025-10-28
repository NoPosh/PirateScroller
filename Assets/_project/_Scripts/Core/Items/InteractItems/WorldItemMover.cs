using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.Items
{
    public class WorldItemMover
    {
        [Header("Movement Settings")]
        [SerializeField] private float _attractionForce = 10f;
        [SerializeField] private float _maxSpeed = 5f;

        private Rigidbody2D _rb;

        public WorldItemMover(Rigidbody2D rb, float attractionForce = 10f, float maxSpeed = 5f)
        {
            _rb = rb;
            _attractionForce = attractionForce;
            _maxSpeed = maxSpeed;
        }

        public void FixedUpdate(float fixedUpdate)
        {
            if (_rb.velocity.magnitude > _maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            }
        }

        public void MoveToCharacter(Vector2 direction)
        {
            _rb.AddForce(direction.normalized * _attractionForce);
        }

        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            _rb.AddForce(force, mode);
        }
    }
}