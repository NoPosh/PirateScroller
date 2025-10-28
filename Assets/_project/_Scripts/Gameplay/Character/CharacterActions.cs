using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Health;
using TestGame.Core.Interfaces;
using UnityEngine;

namespace TestGame.Gameplay.Character
{
    public class CharacterActions: ICharacterActions
    {
        private Action<int> _addBomb;
        private Action<int> _heal;
        public CharacterActions(Action<int> bomb, Action<int> heal)
        {
            _addBomb = bomb;
            _heal = heal;
        }

        public void AddBomb(int count) => _addBomb(count);
        public void Heal(int amount) => _heal(amount);
    }
}