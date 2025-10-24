using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using UnityEngine;

public class BombFabric : MonoBehaviour
{
    public static BombFabric Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnBomb(Transform point, GameObject weapon)
    {
        GameObject go = Instantiate(weapon, point.position, Quaternion.identity);
        return go;
    }
}
