using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject      loadScreen;
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
            _instance.loadScreen.SetActive(true);
            _instance.loadScreen.GetComponent<LoadScreenManager>().SetLoadScreen();
        }
        public static void HideRoomLoadScreen()
        {
            _instance.loadScreen.SetActive(false);
        }

    }
}