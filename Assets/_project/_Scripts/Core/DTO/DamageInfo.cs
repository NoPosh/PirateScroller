using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.DTO
{
    public struct DamageInfo
    {
        public readonly int Value;
        //�������� ����� ��� ���������: (�� �������� ���������� � ���� �����������)
        //��� ����� ����
        //��� �����

        public DamageInfo(int value)
        {
            Value = value;
        }
    }
}