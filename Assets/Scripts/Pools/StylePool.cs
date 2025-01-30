using System;
using System.Collections.Generic;
using PCG;
using UnityEngine;

namespace Pools
{
    public class StylePool : MonoBehaviour
    {
        public PcgStyle          style;
        public int               count;
        public bool              isReady;
        private readonly List<GameObject> _wallPool = new List<GameObject>();
        private readonly List<GameObject> _floorPool = new List<GameObject>();
        private void Start()
        {
            foreach (var p in style.wallPrefab)
            {
                for (var i = 0; i < count; i++)
                {
                    var go = Instantiate(p, transform);
                    go.SetActive(false);
                    _wallPool.Add(go);
                }        
            }
            foreach (var p in style.floorPrefab)
            {
                for (var i = 0; i < count; i++)
                {
                    var go = Instantiate(p, transform);
                    go.SetActive(false);
                    _floorPool.Add(go);
                }        
            }
            isReady = true;
        }
        public GameObject GetRandomWall()
        {
            var go = Util.GetRandomItem(_wallPool);
            while (go.activeInHierarchy)
            {
                go = Util.GetRandomItem(_wallPool);
            }

            return go;
        }
        public GameObject GetRandomFloor()
        {
            var go = Util.GetRandomItem(_floorPool);
            while (go.activeInHierarchy)
            {
                go = Util.GetRandomItem(_floorPool);
            }
            return go;
        }
    }
}