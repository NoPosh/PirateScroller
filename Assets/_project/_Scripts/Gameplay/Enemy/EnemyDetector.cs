using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Gameplay.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyDetector : MonoBehaviour
    {
        //Если в области есть Character, то говорим, что он есть
        //Если в области есть Bomb, то храним ближайший

        private void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
        }
    }
}