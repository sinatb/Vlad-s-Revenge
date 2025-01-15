using System;
using System.Collections.Generic;
using PCG;
using Pools;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public int        level;
        public int        room;
        public Generator  generator;
        public ObjectPool enemies;
        public GameObject stylePoolPrefab;
        private static GameManager _instance;
        private List<Room> _rooms;
        
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
            level = 1;
            room = 0;
            _rooms = generator.GenerateRooms();
            _rooms[0].Activate();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                room++;
                _rooms[room-1].Deactivate();
                _rooms[room].Activate();
            }
        }
    }
}