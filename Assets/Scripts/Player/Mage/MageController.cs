﻿using System;
using System.Linq;
using Managers;
using Player.Util;
using Projectiles;
using UnityEngine;

namespace Player.Mage
{
    public class MageController : PlayerController
    {
        private FixedSizeList<SpellShard> _spellShards;
        private PlayerUI _ui;
        private float    _bonusDamage;
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
                             damage + _bonusDamage,
                             true);
            prj.SetActive(true);
            return prjComp;
        }

        public override void Special()
        {
             _bonusDamage = _spellShards.Where(shard => shard == SpellShard.Vis).Sum(shard => 10);
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