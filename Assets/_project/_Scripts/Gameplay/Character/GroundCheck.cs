using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Gameplay.Character
{
    public class GroundCheck : MonoBehaviour
    {
        private int groundContacts = 0;

        [SerializeField] private LayerMask groundLayer;
        private readonly BoxCollider2D boxCollider;

        public bool IsGrounded => groundContacts > 0;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsInGroundLayer(collision))
                groundContacts++;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsInGroundLayer(collision))
                groundContacts--;
        }

        private bool IsInGroundLayer(Collider2D c)
        {
            return ((1 << c.gameObject.layer) & groundLayer) != 0;
        }

    }
}