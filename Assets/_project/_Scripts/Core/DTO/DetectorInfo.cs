using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.DTO
{
    public struct DetectorInfo
    {
        public bool IsCharacterInArea;
        public bool IsBombInArea;

        public Vector2 CharacterPosition;
        public Vector2 BombPosition;

        public DetectorInfo(bool character, bool bomb, Vector2 characterPos, Vector2 bombPos)
        {
            IsCharacterInArea = character;
            IsBombInArea = bomb;
            CharacterPosition = characterPos;
            BombPosition = bombPos;
        }
    }
}