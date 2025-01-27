using Managers;
using Player.Util;
using Projectiles;
using UnityEngine;

namespace Player.Mage
{
    public class MageController : PlayerController
    {
        private CircularList<SpellShard> _spellShards; 
        public override Projectile Attack(float damage)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            var dir = (mouseWorldPos - transform.position).normalized;
            var prj = GameManager.Instance.projectiles.GetPooledObject("Mage-Bolt");
            prj.transform.position = transform.position;
            var prjComp = prj.GetComponent<Projectile>();
            prjComp.Setup(dir,
                             GameManager.Instance.settings.projectileSpeed,
                             damage,
                             true);
            prj.SetActive(true);
            return prjComp;
        }

        public override void Special()
        {
            Debug.Log("Special");
        }

        public override void AdditionalControls()
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                _spellShards.Add(SpellShard.Vis);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                _spellShards.Add(SpellShard.San);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                _spellShards.Add(SpellShard.Ful);
            }
        }
    }
}