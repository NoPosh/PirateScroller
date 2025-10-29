using System.Collections;
using System.Collections.Generic;
using TestGame.Data.Settings;
using UnityEngine;

namespace TestGame.Data.Settings
{
    [CreateAssetMenu(fileName = "Enemy Setting", menuName = "Settings/ Enemy Setting")]
    public class EnemySettings : ScriptableObject
    {
        [Header("Настройки движения")]
        public PhysicalMoveSettings _physicalMoveSettings;

        [Header("Настройки здоровья")]
        public int maxHealth;
    }
}