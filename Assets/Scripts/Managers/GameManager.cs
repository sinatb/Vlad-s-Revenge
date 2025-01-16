using System;
using System.Collections;
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
        private static GameManager _instance;
        private List<Room> _rooms;
        public bool LoadTrigger { private get;  set; }
        
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
        private IEnumerator LoadNextRoom()
        {
            room++;
            UIManager.ShowRoomLoadScreen();
            while (!LoadTrigger)
            {
                yield return null;
            }
            _rooms[room - 1].Deactivate();
            _rooms[room].Activate();
            LoadTrigger = false;
        }
        private IEnumerator Start()
        {
            
            yield return new WaitUntil(() => styles.TrueForAll(s=>s.isReady) &&
                                             enemies.isReady);
            level = 1;
            room = 0;
            _rooms = generator.GenerateRooms();
            _rooms[0].Activate();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                if (room < 4)
                {
                    StartCoroutine(LoadNextRoom());
                }
            }
        }
    }
}