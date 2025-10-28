using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem.Android;

namespace TestGame.Gameplay.Character
{
    [RequireComponent(typeof(Collider2D))]
    public class CharacterInteractor : MonoBehaviour
    {
        private InteractionContext _context;

        public void Init(InteractionContext context)
        {
            _context = context;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact(_context);
            }
        }
    }
}