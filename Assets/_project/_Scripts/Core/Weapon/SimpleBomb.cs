using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Interfaces;
using UnityEngine;

namespace TestGame.Core.Weapon
{
    public class SimpleBomb : BaseBomb
    {
        //Обычная бомба: при спавне начинает обратный отсчет -> взрывается 
        [SerializeField] private float explosionInterval = 3f;

        [SerializeField] private float explosionRadius = 3f;
        [SerializeField] private float maxExplosionForce = 10f;
        [SerializeField] private int explosionDamage = 1;

        private float explosionTimer = 0f;
        private bool _isActivated = false;

        private Rigidbody2D _rb;

        public override void Init()
        {
            _rb = GetComponent<Rigidbody2D>();
            StartExplosionTimer();
        }

        private void StartExplosionTimer()
        {
            _isActivated = true;
        }

        private void Update()
        {
            if (_isActivated)
            {
                explosionTimer += Time.deltaTime;
            }

            if (explosionTimer >= explosionInterval)
            {
                Explode();
                _isActivated = false;
                Destroy(gameObject);
            }
        }

        private void Explode()
        {
            Debug.Log("Взрыв");
            //Создает сферу вокруг себя, наносит урон, отталкивает кого надо
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject == gameObject) continue;

                if (hit.TryGetComponent(out IDamageable damageable))
                {
                    DamageInfo info = new DamageInfo(explosionDamage);
                    damageable.TakeDamage(info);
                    Debug.Log("Нанесли урон: " + hit.gameObject.name);
                }

                if (hit.TryGetComponent(out IForcable forcable))
                {
                    //То отбрасываем от центра взрыва в сторону
                    Vector2 direction = (hit.transform.position - transform.position).normalized;   //Нам надо направление умножить на силу, причем сила зависит от
                                                                                                    //дальности нахождения объекта
                    float distance = Vector2.Distance(hit.transform.position, transform.position);
                    float force = Mathf.Clamp01(distance / explosionRadius);
                    float finalForce = maxExplosionForce * (1f - force);

                    forcable.AddForce(direction * finalForce, ForceMode2D.Impulse);
                }

            }
        }

        public override void AddForce(Vector2 force, ForceMode2D mode)
        {
            _rb.AddForce(force, mode);
        }

        public override GameObject GetPrefab()
        {
            return gameObject;
        }
    }
}