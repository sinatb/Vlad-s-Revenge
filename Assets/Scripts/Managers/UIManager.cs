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
        [SerializeField] private GameObject      pauseScreen;
        [SerializeField] private GameObject      loseScreen;
        [SerializeField] private GameObject      evolutionScreen;
        public TextMeshProUGUI                   damageCost;
        public TextMeshProUGUI                   healthCost;
        public TextMeshProUGUI                   evolutionBlood;
        public List<PlayerClassData>             playerClasses;
        public PlayerClassData                   selectedClassData;
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
            GameManager.Instance.IsGameRunning = false;
            _instance.loadScreen.SetActive(true);
            _instance.loadScreen.GetComponent<LoadScreenManager>().SetupLoadScreen();
        }
        public static void ShowLevelLoadScreen()
        {
            GameManager.Instance.IsGameRunning = false;
            IsInPerkSelect = true;
            _instance.loadScreen.SetActive(true);
            _instance.loadScreen.GetComponent<LoadScreenManager>().SetupLevelLoadScreen();
        }
        public static void HideRoomLoadScreen()
        {
            _instance.loadScreen.SetActive(false);
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
        public void TogglePauseUI()
        {
            pauseScreen.SetActive(!pauseScreen.activeInHierarchy);
        }
        public static void ShowLoseScreen()
        {
            //code getting shitier :)
            Instance.loseScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                "Level " + GameManager.Instance.level + "-" + GameManager.Instance.room;
            Instance.loseScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                "Blood " + GameManager.Instance.player.blood;
            Instance.loseScreen.SetActive(true);
        }
        public void OnExitClick()
        {
            Application.Quit();
        }
        public void OnResumeClick()
        {
            GameManager.Instance.ResumeGame();
        }
        public void OnMenuClick()
        {
            GameManager.Instance.ResetGame();
            pauseScreen.SetActive(false);
            loseScreen.SetActive(false);
            startScreen.SetActive(true);
        }
        public void OnEvolutionClick()
        {
            startScreen.SetActive(false);
            healthCost.text = GameManager.Instance.healthUpgradeCost + " blood";
            damageCost.text = GameManager.Instance.damageUpgradeCost + " blood";
            evolutionBlood.text ="blood : " + GameManager.Instance.blood;
            evolutionScreen.SetActive(true);
        }
        public void OnReturnClick()
        {
            startScreen.SetActive(true);
            evolutionScreen.SetActive(false);
        }
        public void OnIncreaseClick(int stat)
        {
            if (stat == 1)
            {
                if (GameManager.Instance.blood >= GameManager.Instance.healthUpgradeCost)
                {
                    GameManager.Instance.blood -= (int)GameManager.Instance.healthUpgradeCost;
                    GameManager.Instance.healthUpgradeCost *= 1.5f;
                    GameManager.Instance.healthBonus += 5;
                }
            }else if (stat == 2)
            {
                if (GameManager.Instance.blood >= GameManager.Instance.damageUpgradeCost)
                {
                    GameManager.Instance.blood -= (int)GameManager.Instance.damageUpgradeCost;
                    GameManager.Instance.healthUpgradeCost *= 1.5f;
                    GameManager.Instance.damageBonus += 5;
                }
            }
        }
    }
}