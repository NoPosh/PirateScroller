using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using UnityEngine;

namespace TestGame.Core.Items
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicalItemHandler : MonoBehaviour, IForcable
    {
        [SerializeField] private float maxSpeed = 10f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            _rb.AddForce(force, mode);
        }

        private void FixedUpdate()
        {
            if (_rb.velocity.magnitude > maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * maxSpeed;
            }
        }
    }
}