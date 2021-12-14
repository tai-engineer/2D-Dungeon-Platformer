using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP2D
{
    public class ParticleCollision : MonoBehaviour
    {
        ParticleSystem _particle;
        List<ParticleCollisionEvent> _collisionEvents;
        [SerializeField] GameObject _splatPrefab;

        [MinMaxRange(0.1f, 1f)]
        [SerializeField] RangeFloat _splatLifeTime;
        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
            _collisionEvents = new List<ParticleCollisionEvent>();
        }
        private void OnParticleCollision(GameObject other)
        {
            int count = _particle.GetCollisionEvents(other, _collisionEvents);

            foreach(var collision in _collisionEvents)
            {
                GameObject obj = Instantiate(_splatPrefab, collision.intersection, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                Destroy(obj, Random.Range(_splatLifeTime.minValue, _splatLifeTime.maxValue));
            }
        }
    }
}
