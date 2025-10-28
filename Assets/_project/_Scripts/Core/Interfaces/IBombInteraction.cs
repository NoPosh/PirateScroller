using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Weapon;
using UnityEngine;

namespace TestGame.Core.Interfaces
{
    public interface IBombInteraction
    {
        public event Action OnBombInteraction;
        public void InteractBomb(BaseBomb bomb);
        public void Update(float deltaTime);

    }
}