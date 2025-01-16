﻿using System.Collections;
using System.Collections.Generic;
using PCG;
using Pools;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public int                 level;
        public int                 room;
        public Generator           generator;
        public ObjectPool          enemies;
        public List<StylePool>     styles;

        
        public Room ActiveRoom { get; private set; }
        public bool LoadTrigger { private get;  set; }
        public static GameManager Instance => _instance;
        private static GameManager _instance;
        
        private List<Room>         _rooms;
        
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

        private IEnumerator LoadNextLevel()
        {
            UIManager.ShowLevelLoadScreen();
            level++;
            room = 0;
            _rooms = generator.GenerateRooms();
            yield return new WaitUntil(() => LoadTrigger);
            ActiveRoom?.Deactivate();
            ActiveRoom = _rooms[room];
            ActiveRoom.Activate();
            room++;
            yield return new WaitUntil(() => !UIManager.IsInPerkSelect);
            LoadTrigger = false;
        }
        private IEnumerator LoadNextRoom()
        {
            UIManager.ShowRoomLoadScreen();
            yield return new WaitUntil(() => LoadTrigger);
            ActiveRoom?.Deactivate();
            ActiveRoom = _rooms[room];
            ActiveRoom.Activate();
            room++;
            LoadTrigger = false;
        }
        private IEnumerator Start()
        {
            
            yield return new WaitUntil(() => styles.TrueForAll(s=>s.isReady) &&
                                             enemies.isReady);
            level = 1;
            room = 0;
            _rooms = generator.GenerateRooms();
            StartCoroutine(LoadNextRoom());
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.D) && !UIManager.IsInLoad)
            {
                StartCoroutine(room < 5 ? LoadNextRoom() : LoadNextLevel());
            }
        }
    }
}