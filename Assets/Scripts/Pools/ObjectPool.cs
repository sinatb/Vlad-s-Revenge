using System.Collections.Generic;
using Effects;
using Enemies;
using NUnit.Framework;
using UnityEngine;

namespace Pools
{
    [System.Serializable]
    public class StringObjectPair
    {
        public string key;
        public GameObject obj;
    }
    public class ObjectPool : MonoBehaviour
    {
        public List<StringObjectPair>               prefabs;
        public int                                  count;
        public bool                                 isReady;
        private Dictionary<string,List<GameObject>> _pool;

        /// <summary>
        /// Applies an instant effect on all effectable list members
        /// </summary>
        /// <param name="ie"></param>
        public void ApplyEffectOnAll(InstantEffect ie)
        {
            foreach (var slp in _pool)
            {
                foreach (var e in slp.Value)
                {
                    var be = e.GetComponent<BaseEnemy>();
                    if (be != null)
                        be.AddEffect(ie);
                }
            }
        }
        private void Awake()
        {
            _pool = new Dictionary<string, List<GameObject>>();
            foreach (var pair in prefabs)
            {
                var l = new List<GameObject>();
                for (var i = 0; i < count; i++)
                {
                    var o = Instantiate(pair.obj,transform);
                    o.SetActive(false);
                    l.Add(o);
                }
                _pool.Add(pair.key, l);
            }
            isReady = true;
        }
        public GameObject GetPooledObject(string key)
        {
            var l = _pool[key];
            if (l == null)
            {
                return null;
            }
            foreach (var o in l)
            {
                if (!o.activeInHierarchy)
                {
                    return o;
                }
            }
            return null;
        }
    }
}