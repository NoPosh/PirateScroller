using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Data.Settings
{
    [CreateAssetMenu(fileName = "Physical Move Setting", menuName = "Settings/ Physical Move")]
    public class PhysicalMoveSettings : ScriptableObject
    {
        public float MoveForce;
        public float MaxMoveSpeed;
        public float JumpForce;

        public float StopLerp = 0.15f;
    }
}