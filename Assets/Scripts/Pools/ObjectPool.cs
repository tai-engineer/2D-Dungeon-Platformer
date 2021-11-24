using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class ObjectPool: MonoBehaviour
    {
        protected Stack<GameObject> _instances;
        [SerializeField] int _initialPoolSize;
        [SerializeField] GameObject _prefab;
        protected virtual void Awake()
        {
            _instances = new Stack<GameObject>();
            for (int i = 0; i < _initialPoolSize; i++)
            {
                _instances.Push(CreatePoolObject(false));
            }
        }

        public virtual GameObject Pop(Vector3 position, bool useLocal)
        {
            GameObject poolObject;
            if (_instances.Count > 0)
            {
                poolObject = _instances.Pop();
                poolObject.SetActive(true);
            }
            else
            {
                poolObject = CreatePoolObject(true);
            }

            if (useLocal)
            {
                poolObject.transform.localPosition = position;
            }
            else
            {
                poolObject.transform.position = position;
            }

            return poolObject;
        }
        public void Return(GameObject poolObject)
        {
            poolObject.gameObject.SetActive(false);
            _instances.Push(poolObject);
        }
        GameObject CreatePoolObject(bool active)
        {
            var obj = Instantiate(_prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(active);
            return obj;
        }
    }
}