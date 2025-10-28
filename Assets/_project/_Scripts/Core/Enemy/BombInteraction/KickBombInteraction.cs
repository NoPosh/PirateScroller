using System.Collections;
using System.Collections.Generic;
using TestGame.Core.Interfaces;
using TestGame.Core.Weapon;
using UnityEngine;
using System;

namespace TestGame.Core.Enemy
{
    public class KickBombInteraction: IBombInteraction
    {
        private float _kickForce;
        private readonly Transform transform;

        private float interactionCooldown = 2f;
        private float interactionTimer = 0f;

        public event Action OnBombInteraction;

        public KickBombInteraction(Transform tr, float kickForce = 30f)
        {
            transform = tr;
            _kickForce = kickForce;
        }
        //+ надо направдение задать как-то

        public void Update(float deltaTime)
        {
            if (interactionTimer > 0f) interactionTimer -= deltaTime;
        }

        public void InteractBomb(BaseBomb bomb)
        {
            if (interactionTimer <= 0f)
            {
                IForcable forcable = bomb.gameObject.GetComponent<IForcable>();

                Vector2 dir = (Vector2)bomb.transform.position - (Vector2)transform.position;
                forcable.AddForce(dir * _kickForce, ForceMode2D.Impulse);
                interactionTimer = interactionCooldown;

                OnBombInteraction?.Invoke();
            }

        }
    }
}