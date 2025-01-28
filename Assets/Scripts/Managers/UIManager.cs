using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject      loadScreen;
        [SerializeField] private GameObject      startScreen;
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
            StartCoroutine(GameManager.Instance.StartGame());
            startScreen.SetActive(false);
        }

        public void OnExitClick()
        {
            Application.Quit();
        }
    }
}