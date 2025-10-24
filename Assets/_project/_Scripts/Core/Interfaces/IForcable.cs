using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.Interfaces
{
    public interface IForcable
    {
        public void AddForce(Vector2 force, ForceMode2D mode);
    }
}
