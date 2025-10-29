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

    public struct OnToggleGamePause
    {

    }

    public struct OnGameStateChange
    {
        public readonly bool IsPaused;
        public OnGameStateChange(bool tog)
        {
            IsPaused = tog;
        }
    }

    public struct OnGameOver
    {

    }
}