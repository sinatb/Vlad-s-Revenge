using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        public Slider      health;
        public Image       avatar;
        public List<Image> perks;
        public Image       coolDownImage;
        public Image       specialImage;
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
    }
}