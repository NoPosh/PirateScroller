using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TestGame.Core.Interfaces;
using TestGame.Core.Weapon;

namespace TestGame.Gameplay.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyCharacterDetector : MonoBehaviour
    {
        public event Action OnCharacterEnter;
        public event Action OnCharacterExit;

        public bool HasCharacter => _characterTransform != null;
        public Vector2 CharacterPosition => _characterTransform != null ? (Vector2)_characterTransform.position : Vector2.zero;
        public IDamageable CurrentCharacterDamageable;
        public IForcable CharacterForcable;

        private Transform _characterTransform;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Character"))
            {
                _characterTransform = collision.transform;
                CurrentCharacterDamageable = collision.gameObject.GetComponent<IDamageable>();
                CharacterForcable = collision.gameObject.GetComponent<IForcable>();

                OnCharacterEnter?.Invoke(); //Так-то надо просто пустить событие, чтобы сверху компонент обновился
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Character"))
            {
                _characterTransform = null;
                CurrentCharacterDamageable = null;
                CharacterForcable = null;

                OnCharacterExit?.Invoke();
            }
        }
    }
}