using UnityEngine;
using UnityEngine.Events;
using System;
namespace DP2D
{
    public class Damageable : MonoBehaviour
    {
        [Serializable]
        public class DamageEvent: UnityEvent<Damager, Damageable> { }

        public DamageEvent OnTakeDamage;
        public void TakeDamage(Damager damager)
        {
            OnTakeDamage.Invoke(damager, this);
        }
    }
}
