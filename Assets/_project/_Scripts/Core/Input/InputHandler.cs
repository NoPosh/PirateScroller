using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;


namespace TestGame.Core.Input
{
    public class InputHandler
    {
        public event Action<Vector2> OnMove;
        public event Action OnThrowBomb;

        private InputS _controls;

        private Vector2 _moveInput;

        public InputHandler()
        {
            _controls = new InputS();
            _controls.Player.Enable();
        }

        public void Update()
        {
            _moveInput = _controls.Player.Move.ReadValue<Vector2>();

            if (_moveInput != Vector2.zero)
            {
                OnMove?.Invoke(_moveInput);
            }
            else
            {
                OnMove?.Invoke(Vector2.zero);
            }
        }
    }
}