using TMPro;
using UnityEngine;

namespace Managers
{
    public class LoadScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject      defaultLoadScreen;
        [SerializeField] private GameObject      levelLoadScreen;
        [SerializeField] private TextMeshProUGUI levelCompleteText;
        [SerializeField] private TextMeshProUGUI roomText;
        [SerializeField] private TextMeshProUGUI levelText;
        private Animator _animator;


        public void SetupLoadScreen()
        {
            defaultLoadScreen.SetActive(true);
            
            if (_animator == null)
                _animator = GetComponent<Animator>();
            
            _animator.SetBool("StartFade", true);
            roomText.text = "Room " + (GameManager.Instance.room + 1);
            levelText.text = "Level " + GameManager.Instance.level;
        }

        public void SetupLevelLoadScreen()
        {
            levelLoadScreen.SetActive(true);
            
            if (_animator == null)
                _animator = GetComponent<Animator>();
            
            var congratsText = GameManager.Instance.level == 0 ? 
                "Welcome To The Prison" :
                GameManager.Instance.level.ToString();
            
            levelCompleteText.text = congratsText == "Welcome To The Prison" ?
                congratsText :
                "Level " + congratsText + " Complete!";
            _animator.SetBool("StartLevelFade", true);
        }

        public void OnPerkSelect()
        {
            _animator.SetBool("StartLevelFade", false);
            _animator.SetBool("SelectedPerk", true);
            UIManager.IsInPerkSelect = false;
        }
        public void ActivateLoadTrigger()
        {
            GameManager.Instance.LoadTrigger = true;
        }
        public void DeactivateLoadScreen()
        {
            UIManager.HideRoomLoadScreen();
            levelLoadScreen.SetActive(false);
            defaultLoadScreen.SetActive(false);
        }
    }
}