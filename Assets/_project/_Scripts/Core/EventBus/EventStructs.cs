using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.EventBus
{
    public struct EventStructs
    {

    }

    public struct BombAmountChange
    {
        public readonly int Amount;
        public BombAmountChange(int amount)
        {
            Amount = amount;
        }
    }

    public struct OnCharacterHealthChange
    {
        public readonly int CurrentHealth;
        public OnCharacterHealthChange(int amount)
        {
            CurrentHealth = amount;
        }

    }
}