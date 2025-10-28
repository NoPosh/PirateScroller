using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestGame.Core.Interfaces;
using TestGame.Core.Weapon;
using UnityEngine;

namespace TestGame.Gameplay.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyBombDetector : MonoBehaviour
    {
        public event Action OnBombEnter;
        public event Action OnBombExit;

        public bool HasBombs => _bombsInArea.Count > 0;
        public BaseBomb FirstBomb;
        public Vector2 FirstBombPosition => HasBombs ? (Vector2)_bombsInArea[0].transform.position : Vector2.zero;

        private readonly List<BaseBomb> _bombsInArea = new List<BaseBomb>();


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<BaseBomb>(out var bomb))
            {
                AddBomb(bomb);

                OnBombEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

            if (collision.TryGetComponent(out BaseBomb bomb))
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
            FirstBomb = _bombsInArea[0].GetComponent<BaseBomb>();

            bomb.OnDestroyed += OnBombDestroyed;
        }

        private void RemoveBomb(BaseBomb bomb)
        {
            if (bomb == null)
                return;

            _bombsInArea.Remove(bomb);

            if (_bombsInArea.Count > 0)
                FirstBomb = _bombsInArea[0].GetComponent<BaseBomb>();
            else FirstBomb = null;

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
