using System.Collections;
using System.Collections.Generic;
using PCG;
using Pools;
using Settings;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //------public variables------
        public int                 level;
        public int                 room;
        public Generator           generator;
        public ObjectPool          enemies;
        public ObjectPool          projectiles;
        public List<StylePool>     styles;
        public Player.Player       player;
        public GameSettings        settings;
        //------properties------------
        public Room ActiveRoom { get; private set; }
        public bool LoadTrigger { private get;  set; }
        public bool IsGameRunning { get; set; }

        public static GameManager Instance => _instance;
        private static GameManager _instance;
        //------private variables-----
        private List<Room>         _rooms;
        
        //WARNING Creates coupling between GameManager and Room
        public static StylePool GetStylePool(PcgStyle style)
        {
            return _instance.styles.Find(s => s.style == style);
        }

        #region Level loading
        /// <summary>
        /// Coroutine to load the next level. Waits in the perk select screen
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadNextLevel()
        {
            UIManager.ShowLevelLoadScreen();
            level++;
            room = 0;
            _rooms = generator.GenerateRooms();
            yield return new WaitUntil(() => LoadTrigger);
            if (level == 1 && room == 0)
            {
                player.SetUpPlayer(UIManager.Instance.selectedClassData);
            }
            ActiveRoom?.Deactivate();
            ActiveRoom = _rooms[room];
            ActiveRoom.Activate();
            room++;
            player.transform.position = ActiveRoom.GetRandomFloor();
            yield return new WaitUntil(() => !UIManager.IsInPerkSelect);
            LoadTrigger = false;
            IsGameRunning = true;
        }
        /// <summary>
        /// Loads next room in the level. 2 seconds delay before player can be in control
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadNextRoom()
        {
            UIManager.ShowRoomLoadScreen();
            yield return new WaitUntil(() => LoadTrigger);
            ActiveRoom?.Deactivate();
            ActiveRoom = _rooms[room];
            ActiveRoom.Activate();
            room++;
            player.transform.position = ActiveRoom.GetRandomFloor();
            LoadTrigger = false;
            IsGameRunning = true;
        }
        #endregion

        public void PauseGame()
        {
            Time.timeScale = 0.0f;
            UIManager.Instance.TogglePauseUI();
            IsGameRunning = false;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1.0f;
            UIManager.Instance.TogglePauseUI();
            IsGameRunning = true;
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
        
        public IEnumerator StartGame()
        {
            yield return new WaitUntil(() => styles.TrueForAll(s=>s.isReady) &&
                                             enemies.isReady &&
                                             projectiles.isReady);
            level = 0;
            room = 0;
            StartCoroutine(LoadNextLevel());
        }
        private void Update()
        {
            if (IsGameRunning && (!ActiveRoom.hasEnemy))
            {
                StartCoroutine(room < 5 ? LoadNextRoom() : LoadNextLevel());
            }
        }
    }
}