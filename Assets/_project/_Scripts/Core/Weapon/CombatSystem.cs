using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TestGame.Core.EventBus;
using TestGame.Core.Interfaces;
using UnityEngine;

namespace TestGame.Core.Weapon
{
    public class CombatSystem
    {
        private Transform _spawnPoint;
        private int _bombsAmount = 0;
        private BaseBomb _baseBomb;
        private float _throwForce;

        public CombatSystem(Transform spawnPoint, float throwForce = 50f)
        {
            _spawnPoint = spawnPoint;
            _throwForce = throwForce;

            EventBus.EventBus.Raise(new BombAmountChange(_bombsAmount));
        }

        public void SetWeapon(BaseBomb weapon)
        {
            _baseBomb = weapon; 
        }

        public void AddBomb(int amount = 1)
        {
            _bombsAmount += amount;
            EventBus.EventBus.Raise(new BombAmountChange(_bombsAmount));
            Debug.Log("Добавили бомбу");
        }

        public void ThrowBomb(Vector3 direction)
        {
            GameObject go = SpawnBomb();
            if (go == null) return;

            if (go.TryGetComponent(out IForcable forcable))
            {
                Vector3 dir = direction - _spawnPoint.position;
                forcable.AddForce(dir.normalized * _throwForce, ForceMode2D.Impulse);
            }           
        }

        public void PutBomb()
        {
            SpawnBomb();
        }

        private GameObject SpawnBomb()
        {
            if (_bombsAmount > 0)
            {
                _bombsAmount--;
                EventBus.EventBus.Raise(new BombAmountChange(_bombsAmount));

                GameObject go = BombFabric.Instance.SpawnBomb(_spawnPoint, _baseBomb.GetPrefab());
                BaseBomb bomb = go.GetComponent<BaseBomb>();
                bomb.Init();
                return go;
            }
            return null;
        }
    }
}