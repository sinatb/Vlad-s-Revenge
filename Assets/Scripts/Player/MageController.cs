using Managers;
using Projectiles;
using UnityEngine;

namespace Player
{
    public class MageController : PlayerController
    {
        public override void Attack()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            var dir = (mouseWorldPos - transform.position).normalized;
            var prj = GameManager.Instance.projectiles.GetPooledObject("Mage-Bolt");
            prj.transform.position = transform.position;
            prj.GetComponent<Projectile>().Setup(dir,
                                                    GameManager.Instance.settings.projectileSpeed,
                                                    GameManager.Instance.player.classData.damage);
            prj.SetActive(true);
        }
    }
}