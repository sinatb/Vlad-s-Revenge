using System;
using System.Collections.Generic;
using Player.Mage;
using Player.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        public Slider           health;
        public TextMeshProUGUI  blood;
        public Image            avatar;
        public List<Image>      perks;
        public Image            coolDownImage;
        public Image            specialImage;
        public GameObject       mageSpecialUI;
        public Sprite           san;
        public Sprite           vis;
        public Sprite           ful;
        private Player          _player;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        public bool        CanSpecial { get; private set; } = true;
        public IEnumerator<WaitForSeconds> SpecialCooldown(float time)
        {
            var timeElapsed = 0.0f;
            CanSpecial = false;
            coolDownImage.fillAmount = 1;
            while (timeElapsed < time)
            {
                yield return new WaitForSeconds(0.01f);
                timeElapsed += 0.01f;
                coolDownImage.fillAmount = 1 - timeElapsed / time;
            }

            CanSpecial = true;
        }

        public void UpdateMageSpecialUI(FixedSizeList<SpellShard> shards)
        {
            if (mageSpecialUI.activeSelf == false)
                throw new System.Exception("Mage Special UI is not active.");
            for (var i=0; i<3; i++)
            {
                var image = mageSpecialUI.transform.GetChild(i).GetChild(0).GetComponent<Image>();
                switch (shards[i])
                {
                    case SpellShard.Ful:
                        image.sprite = ful;
                        break;
                    case SpellShard.San:
                        image.sprite = san;
                        break;
                    case SpellShard.Vis:
                        image.sprite = vis;
                        break;
                }
            }
        }

        private void Update()
        {
            if (_player.MaximumHealth != 0)
                health.value = _player.Health/_player.MaximumHealth;
            blood.text = "Blood : " + _player.blood;
        }
    }
}