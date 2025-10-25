using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestGame.Core.Weapon;
using UnityEngine;

namespace TestGame.Gameplay.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyDetector : MonoBehaviour
    {
        public event Action<Vector2> OnCharacterEnter;   //Персонаж зашел -> передаем его Transform 
        public event Action OnCharacterExit;    //Персонаж вышел -> обнуляем
        public event Action<BaseBomb> OnBombEnter;
        public event Action OnBombExit;

        public bool HasCharacter => _characterTransform != null;
        public Vector2 CharacterPosition => _characterTransform != null ? (Vector2)_characterTransform.position : Vector2.zero;

        public bool HasBombs => _bombsInArea.Count > 0;
        public BaseBomb FirstBomb => _bombsInArea.Count > 0 ? _bombsInArea[0] : null;

        public Vector2 FirstBombPosition => FirstBomb != null ? (Vector2)FirstBomb.transform.position : Vector2.zero;        

        private readonly List<BaseBomb> _bombsInArea = new List<BaseBomb>();
        private Transform _characterTransform;

        public BaseBomb GetNearestBomb()
        {
            CleanupNullBombs();

            if (_bombsInArea.Count == 0)
                return null;

            return _bombsInArea.OrderBy(b => Vector2.Distance(transform.position, b.transform.position)).FirstOrDefault();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Character"))
            {
                _characterTransform = collision.transform;
                OnCharacterEnter?.Invoke(collision.transform.position);
            }
            else if (collision.TryGetComponent<BaseBomb>(out var bomb))
            {
                AddBomb(bomb);
                OnBombEnter?.Invoke(bomb);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Character"))
            {
                 _characterTransform = null;
                 OnCharacterExit?.Invoke();                
            }
            else if (collision.TryGetComponent(out BaseBomb bomb))
            {
                RemoveBomb(bomb);
                if (_bombsInArea.Count == 0)
                    OnBombExit?.Invoke();
            }
        }

        private void AddBomb(BaseBomb bomb)
        {
            if (bomb == null || _bombsInArea.Contains(bomb))
                return;

            _bombsInArea.Add(bomb);

            bomb.OnDestroyed += OnBombDestroyed;
        }

        private void RemoveBomb(BaseBomb bomb)
        {
            if (bomb == null)
                return;

            _bombsInArea.Remove(bomb);
            bomb.OnDestroyed -= OnBombDestroyed;
        }

        private void OnBombDestroyed(BaseBomb bomb)
        {
            RemoveBomb(bomb);
        }

        private void CleanupNullBombs()
        {
            _bombsInArea.RemoveAll(b => b == null);
        }

        private void OnDestroy()
        {
            foreach (var bomb in _bombsInArea)
            {
                if (bomb != null)
                    bomb.OnDestroyed -= OnBombDestroyed;
            }

            _bombsInArea.Clear();
        }
    }
}