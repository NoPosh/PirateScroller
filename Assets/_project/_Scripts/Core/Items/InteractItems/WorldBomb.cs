using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Interfaces;
using TestGame.Core.Movement;
using UnityEngine;

namespace TestGame.Core.Items
{
    public class WorldBomb : MonoBehaviour, IInteractable, IForcable
    {
        public string InteractionPromt { get; }

        [SerializeField] private float pickUpDistance = 0.3f;
        [SerializeField] private float breakingDistance = 3f;
        private Transform _currentTargetPos;
        private WorldItemMover _itemMover;

        private Rigidbody2D _rb;
        private Collider2D _collider;

        private Action _action;
       
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();

            _itemMover = new WorldItemMover(_rb);
        }

        public void Interact(InteractionContext context)
        {
            _currentTargetPos = context.Interactor.transform;
            _action = () => context.CharacterActions.AddBomb(1);
        }

        private void FixedUpdate()
        {
            _itemMover.FixedUpdate(Time.fixedDeltaTime);

            if (_currentTargetPos != null)
            {
                Vector2 dir = _currentTargetPos.position - transform.position;
                _itemMover.MoveToCharacter(dir);

                if (dir.magnitude <= pickUpDistance)
                {
                    _action();
                    Destroy(gameObject);
                }

                if (dir.magnitude >= breakingDistance)
                {
                    _currentTargetPos = null;
                }
            }           
        }

        public void AddForce(Vector2 force, ForceMode2D mode)
        {
            _itemMover.AddForce(force, mode);
        }

    }
}