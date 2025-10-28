using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGame.Core.Interfaces;

namespace TestGame.Core.DTO
{
    public class InteractionContext
    {
        public GameObject Interactor;
        public ICharacterActions CharacterActions;

        public InteractionContext(GameObject interactor, ICharacterActions characterActions)
        {
            Interactor = interactor;
            CharacterActions = characterActions;
        }
    }
}