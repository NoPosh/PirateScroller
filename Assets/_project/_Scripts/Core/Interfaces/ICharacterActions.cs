using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.Interfaces
{
    public interface ICharacterActions
    {
        void AddBomb(int count);
        void Heal(int amount);
    }
}