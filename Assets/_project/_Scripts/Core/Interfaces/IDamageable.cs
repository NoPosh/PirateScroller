using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using UnityEngine;

namespace TestGame.Core.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(DamageInfo info);
    }
}