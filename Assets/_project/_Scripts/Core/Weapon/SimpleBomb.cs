using System.Collections;
using System.Collections.Generic;
using TestGame.Core.DTO;
using TestGame.Core.Interfaces;
using UnityEngine;

namespace TestGame.Core.Weapon
{
    public class SimpleBomb : MonoBehaviour
    {
        //������� �����: ��� ������ �������� �������� ������ -> ���������� 
        [SerializeField] private float explosionInterval = 3f;

        [SerializeField] private float explosionRadius = 3f;
        [SerializeField] private float maxExplosionForce = 10f;
        [SerializeField] private int explosionDamage = 1;

        private float explosionTimer = 0f;
        private bool _isActivated = false;

        private void Start()
        {
            Init();
        }
        public void Init()
        {
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
            Debug.Log("�����");
            //������� ����� ������ ����, ������� ����, ����������� ���� ����
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject == gameObject) continue;

                if (hit.TryGetComponent(out IDamageable damageable))
                {
                    DamageInfo info = new DamageInfo(explosionDamage);
                    damageable.TakeDamage(info);
                    Debug.Log("������� ����: " + hit.gameObject.name);
                }

                if (hit.TryGetComponent(out IForcable forcable))
                //if (hit.attachedRigidbody != null)
                {
                    //�� ����������� �� ������ ������ � �������
                    Vector2 direction = (hit.transform.position - transform.position).normalized;   //��� ���� ����������� �������� �� ����, ������ ���� ������� ��
                                                                                                    //��������� ���������� �������
                    float distance = Vector2.Distance(hit.transform.position, transform.position);
                    float force = Mathf.Clamp01(distance / explosionRadius);
                    float finalForce = maxExplosionForce * (1f - force);

                    forcable.AddForce(direction * finalForce, ForceMode2D.Impulse);
                    //hit.attachedRigidbody.AddForce(direction * finalForce, ForceMode2D.Impulse);
                    Debug.Log("���������: " + hit.gameObject.name);
                }

            }
        }
    }
}