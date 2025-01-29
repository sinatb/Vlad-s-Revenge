using Combat;
using Managers;
using UnityEngine;

namespace Player.Hunter
{
    public class HunterController : PlayerController
    {
        public override void Attack(PlayerAttackData data)
        {
            var dir = GetMouseVector();
            var prj = GameManager.Instance.projectiles.GetPooledObject("Hunter-Bolt");
            prj.SetActive(true);
            prj.transform.position = transform.position;
            var prjComp = prj.GetComponent<HunterProjectile>();
            prjComp.Setup(dir,
                GameManager.Instance.settings.projectileSpeed);
            prjComp.SetPlayerAttackData(data);
        }

        public override void Special()
        {
            var dir = GetMouseVector();
            var prj = GetPooledProjectile("Hunter-Bolt") as HunterProjectile;
            prj!.Setup(dir, GameManager.Instance.settings.projectileSpeed);
            var player = GetComponent<Player>();
            prj.SetPlayerAttackData(player.CalculateAttackData());
            prj.MakeSpecial();
        }

        public override void AdditionalControls()
        {
            return;
        }
    }
}