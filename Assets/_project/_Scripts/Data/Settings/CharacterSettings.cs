using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Weapon;
using UnityEngine;

namespace TestGame.Data.Settings
{
    [CreateAssetMenu(fileName = "Character Setting", menuName = "Settings/ Character Setting")]
    public class CharacterSettings : ScriptableObject
    {
        [Header("��������� ��������")]
        public PhysicalMoveSettings _physicalMoveSettings;

        [Header("��������� ��������")]
        public int maxHealth;

        [Header("��������� ������")]
        public BaseBomb startBomb;

        [Header("������")]
        public float _throwForce;
    }
}