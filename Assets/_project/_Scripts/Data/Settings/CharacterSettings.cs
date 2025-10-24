using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Weapon;
using UnityEngine;

namespace TestGame.Data.Settings
{
    [CreateAssetMenu(fileName = "Character Setting", menuName = "Settings/ Character Setting")]
    public class CharacterSettings : ScriptableObject
    {
        [Header("Настройки движения")]
        public PhysicalMoveSettings _physicalMoveSettings;

        [Header("Настройки здоровья")]
        public int maxHealth;

        [Header("Настройки оружия")]
        public BaseBomb startBomb;

        [Header("Прочее")]
        public float _throwForce;
    }
}