using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using UnityEngine;

namespace TestGame.Core.Interfaces
{
    public interface IInteractable
    {
        public string InteractionPromt {  get; }
        public void Interact(InteractionContext context);
    }
}