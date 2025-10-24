using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.Weapon
{
    public abstract class BaseBomb : MonoBehaviour
    {
        public abstract void Init();
        public abstract void AddForce(Vector2 force, ForceMode2D mode);
        public abstract GameObject GetPrefab();
    }
}