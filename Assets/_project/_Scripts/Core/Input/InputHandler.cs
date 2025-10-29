using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Core.EventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


namespace TestGame.Core.Input
{
    public class InputHandler
    {
        public event Action<Vector2> OnMove;
        public event Action OnJump;
        public event Action<Vector3> OnThrowBomb;
        public event Action OnPutBomb;

        private InputS _controls;

        private Vector2 _moveInput;
        private bool _needJump;

        private bool _needThrowBomb;
        private bool _needPutBomb;
        private bool _pauseGame;

        public InputHandler()
        {
            _controls = new InputS();
            _controls.Player.Enable();
        }

        public void Update()
        {
            _moveInput = _controls.Player.Move.ReadValue<Vector2>();
            _needJump = _controls.Player.Jump.IsPressed();
            _pauseGame = _controls.Player.ESC.triggered;

            _needThrowBomb = _controls.Player.LMB.triggered;
            _needPutBomb = _controls.Player.RMB.triggered;

            if (_pauseGame)
            {
                EventBus.EventBus.Raise(new OnToggleGamePause());
                _pauseGame = false;
                return;
            }

            if (Level.LevelManager.Instance.IsPaused) return;

            if (_moveInput != Vector2.zero)
            {
                OnMove?.Invoke(_moveInput);
            }
            else
            {
                OnMove?.Invoke(Vector2.zero);
            }
            
            if (_needJump)
            {
                OnJump?.Invoke();
            }

            if (_needThrowBomb)
            {
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
                mouseWorldPos.z = 0f;

                OnThrowBomb?.Invoke(mouseWorldPos);
            }

            if (_needPutBomb)
            {
                OnPutBomb?.Invoke();
            }

        }

        public void Disable()
        {
            _controls.Disable();
        }

    }
}