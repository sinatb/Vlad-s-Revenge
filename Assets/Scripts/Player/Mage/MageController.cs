using System;
using System.Linq;
using Combat;
using Managers;
using Player.Util;
using UnityEngine;

namespace Player.Mage
{
    public class MageController : PlayerController
    {
        private FixedSizeList<SpellShard> _spellShards;
        private PlayerUI _ui;
        private float    _bonusDamage;
        private float    _bonusLifeSteal;
        private int      _bonusAoe;
        private void Awake()
        {
            _ui = gameObject.GetComponent<PlayerUI>();
            _spellShards = new FixedSizeList<SpellShard>(3);
            for (int i = 0; i < 3; i++)
            {
                _spellShards.Add(SpellShard.Vis);
            }
            _ui.UpdateMageSpecialUI(_spellShards);
        }

        public override void Attack(PlayerAttackData data)
        {
            //Calculating projectile direction
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            var dir = (mouseWorldPos - transform.position).normalized;
            //Getting the projectile from the object pool and doing some basic setup
            var prj = GameManager.Instance.projectiles.GetPooledObject("Mage-Bolt");
            prj.SetActive(true);
            prj.transform.position = transform.position;
            var prjComp = prj.GetComponent<MageProjectile>();
            prjComp.Setup(dir,
                             GameManager.Instance.settings.projectileSpeed);
            //Updating the data with the bonus values
            prjComp.SetPlayerAttackData(data);
            prjComp.PlayerAttack.AddDamage(_bonusDamage);
            prjComp.PlayerAttack.AddLifeSteal(_bonusLifeSteal);
            prjComp.Aoe = _bonusAoe;
        }

        public override void Special()
        {
             _bonusDamage = _spellShards.Where(shard => shard == SpellShard.Vis).Sum(shard => 10);
             _bonusLifeSteal = _spellShards.Where(shard => shard == SpellShard.San).Sum(shard => 0.05f);
             _bonusAoe = _spellShards.Where(shard => shard == SpellShard.Ful).Sum(shard => 1);
        }

        public override void AdditionalControls()
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                _spellShards.Add(SpellShard.Vis);
                _ui.UpdateMageSpecialUI(_spellShards);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                _spellShards.Add(SpellShard.San);
                _ui.UpdateMageSpecialUI(_spellShards);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                _spellShards.Add(SpellShard.Ful);
                _ui.UpdateMageSpecialUI(_spellShards);
            }
        }
    }
}