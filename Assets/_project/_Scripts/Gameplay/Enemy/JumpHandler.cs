using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Gameplay.Enemy
{
    public class JumpHandler : MonoBehaviour
    {
        private int jumpContacts = 0;
        private readonly BoxCollider2D boxCollider;

        public bool CanJump => jumpContacts > 0;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("JumpArea"))
                jumpContacts++;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("JumpArea"))
                jumpContacts--;
        }
    }
}