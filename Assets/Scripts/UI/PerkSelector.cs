using System;
using Managers;
using Perks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class PerkSelector : MonoBehaviour
    {
        public Image           perk1Image;
        public Image           perk2Image;
        public TextMeshProUGUI description;
        
        private Perk _perk1;
        private Perk _perk2;
        private void OnEnable()
        {
            _perk1 = PerkManager.GetPerk();
            _perk2 = PerkManager.GetPerk();
            
            perk1Image.sprite = _perk1.icon;
            perk2Image.sprite = _perk2.icon;

        }

        public void SelectPerk(int perk)
        {
            if (perk == 1)
            {
                GameManager.Instance.player.AddPerk(_perk1);
            }else if (perk == 2)
            {
                GameManager.Instance.player.AddPerk(_perk2);
            }
            PerkManager.UpdatePerks();
        }
        public void SetDescription(int perk)
        {
            if (perk == 1)
                description.text = _perk1.description;
            else if (perk == 2)
                description.text = _perk2.description;
        }

        public void ResetDescription()
        {
            description.text = "";
        }
    }
}
