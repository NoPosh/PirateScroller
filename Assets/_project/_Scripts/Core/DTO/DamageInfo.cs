using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.DTO
{
    public struct DamageInfo
    {
        public readonly int Value;
        //Например можно еще запомнить: (тк планирую расширение в этом направлении)
        //Кто нанес урон
        //Тип урона

        public DamageInfo(int value)
        {
            Value = value;
        }
    }
}