using UnityEngine;
using UnityEngine.Events;
using System;

namespace DP2D
{
    public class Damager : MonoBehaviour
    {
        public enum RaycastType { Rectangular, Circle };
        [Serializable]
        public class DamageableEvent: UnityEvent<Damager, Damageable> { }
        [Serializable]
        public class NonDamageableEvent: UnityEvent<Damager> { }


        public int damage = 1;

        public RaycastType type;
        public Vector2 offset;
        public Vector2 size = new Vector2(1f,1f);

        [Range(0f, 360f)]
        public float angle = 45f;
        public float radius = 1f;
        [HideInInspector]
        public Vector2 viewDirection = Vector2.left;

        public bool canHitTrigger;
        public bool disableDamageAfterHit = true;
        public LayerMask hittableLayer;
        public DamageableEvent onDamageableHit;
        public NonDamageableEvent onNonDamageableHit;


        ContactFilter2D _contactFilter;
        Collider2D[] _attackOverlapResults = new Collider2D[6];
        bool _canDamage = true;
        void Awake()
        {
            _contactFilter.layerMask = hittableLayer;
            _contactFilter.useLayerMask = true;
            _contactFilter.useTriggers = canHitTrigger;
        }

        void FixedUpdate()
        {
            if (!_canDamage)
                return;

            int hitCount;
            if (type == RaycastType.Rectangular)
            {
                Vector2 scale = transform.lossyScale;
                Vector2 facingOffset = Vector2.Scale(offset, scale);
                Vector2 scaledSize = Vector2.Scale(scale, size);

                Vector2 pointA = (Vector2)transform.position + facingOffset - scaledSize * 0.5f;
                Vector2 pointB = pointA + scaledSize;

                hitCount = Physics2D.OverlapArea(pointA, pointB, _contactFilter, _attackOverlapResults);
                for (int i = 0; i < hitCount; i++)
                {
                    if (_attackOverlapResults[i].TryGetComponent<Damageable>(out Damageable damageable))
                    {
                        damageable.TakeDamage(this);
                        onDamageableHit.Invoke(this, damageable);
                    }
                    else
                    {
                        onNonDamageableHit.Invoke(this);
                    }

                    if (disableDamageAfterHit)
                        DisableDamage();
                }
            }
            else if (type == RaycastType.Circle)
            {
                hitCount = Physics2D.OverlapCircle(transform.position, radius, _contactFilter, _attackOverlapResults);
                
                for (int i = 0; i < hitCount; i++)
                {
                    if (_attackOverlapResults[i].TryGetComponent<Damageable>(out Damageable damageable))
                    {
                        Vector2 distance = _attackOverlapResults[i].transform.position - transform.position;
                        float targetAngle = Vector2.SignedAngle(distance, viewDirection);
                        if (targetAngle < 0)
                            targetAngle = 360 - targetAngle * -1;

                        if (targetAngle > angle)
                            continue;

                        damageable.TakeDamage(this);
                        onDamageableHit.Invoke(this, damageable);
                        if (disableDamageAfterHit)
                            DisableDamage();
                    }
                }
            }
        }

        public void DisableDamage()
        {
            _canDamage = false;
        }

        public void EnableDamage()
        {
            _canDamage = true;
        }
    }
}
