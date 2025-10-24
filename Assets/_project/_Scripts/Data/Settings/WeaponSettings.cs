using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSettings : ScriptableObject
{
    public float explosionInterval = 3f;
    public float explosionRadius = 3f;
    public float maxExplosionForce = 10f;
    public int explosionDamage = 1;
}
