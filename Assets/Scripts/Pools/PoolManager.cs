using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Singleton;
namespace DP2D
{
    public class PoolManager : Singleton<PoolManager>
    {
        static ObjectPool[] _pools;

        protected override void Awake()
        {
            base.Awake();
            _pools = GetComponentsInChildren<ObjectPool>();
        }

        public static T GetPool<T>()
            where T : class
        {
            for (int i = 0; i < _pools.Length; i++)
            {
                T pool = _pools[i] as T;
                if (pool != null)
                    return pool;
            }

            return null;
        }
    }
}
