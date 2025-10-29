using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Data.Settings
{
    [CreateAssetMenu(fileName = "Enemy Attack Setting", menuName = "Settings/ Enemy Attack Setting")]
    public class EnemyAttackSettings : ScriptableObject
    {
        public int _attackDamage = 1;
        public float _attackCooldown = 2f;
    }
}