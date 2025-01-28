using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject      loadScreen;
        [SerializeField] private GameObject      startScreen;
        [SerializeField] private GameObject      heroSelectScreen;
        public List<PlayerClassData>             playerClasses;
        public PlayerClassData                   selectedClassData;
        public static bool IsInLoad;
        public static bool IsInPerkSelect;
        private static UIManager _instance;
        public static UIManager Instance => _instance;
        
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

        public static void ShowRoomLoadScreen()
        {
            IsInLoad = true;
            _instance.loadScreen.SetActive(true);
            _instance.loadScreen.GetComponent<LoadScreenManager>().SetupLoadScreen();
        }
        public static void ShowLevelLoadScreen()
        {
            IsInLoad = true;
            IsInPerkSelect = true;
            _instance.loadScreen.SetActive(true);
            _instance.loadScreen.GetComponent<LoadScreenManager>().SetupLevelLoadScreen();
        }
        public static void HideRoomLoadScreen()
        {
            _instance.loadScreen.SetActive(false);
            IsInLoad = false;
        }

        public void OnStartGameClick()
        {
            startScreen.SetActive(false);
            heroSelectScreen.SetActive(true);
        }

        public void OnSelectHeroClick(int index)
        {
            heroSelectScreen.SetActive(false);
            selectedClassData = playerClasses[index];
            StartCoroutine(GameManager.Instance.StartGame());
        }
        public void OnExitClick()
        {
            Application.Quit();
        }
    }
}