using System.Collections.Generic;
using PCG;
using Pools;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public int        level;
        public Generator  generator;
        public ObjectPool enemies;
        public GameObject stylePoolPrefab;
        private static GameManager _instance;
        
        public static GameManager Instance => _instance;
        public List<StylePool> styles;
        
        public static StylePool GetStylePool(PcgStyle style)
        {
            return _instance.styles.Find(s => s.style == style);
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

        }
        private void Start()
        {
            var r = generator.GenerateRooms();
            r[0].Activate();
        }
    }
}