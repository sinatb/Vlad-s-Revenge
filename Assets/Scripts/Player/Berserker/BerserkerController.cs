using Combat;
using UnityEngine;

namespace Player.Berserker
{
    public class BerserkerController : PlayerController
    {
        public override void Attack(PlayerAttackData data)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            var dir = (mouseWorldPos - transform.position).normalized;
        }

        public override void Special()
        {
            
        }

        public override void AdditionalControls()
        {
            return;
        }
    }
}