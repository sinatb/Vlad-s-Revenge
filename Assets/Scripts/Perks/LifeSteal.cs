﻿using Perks.Interfaces;
using Projectiles;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/LifeSteal", fileName = "LifeSteal")]
    public class LifeSteal : AttackModifierPerk
    {
        public float healPercentage;
        public override void ModifyAttack(Player.Player player, Projectile projectile)
        {
            player.Heal(projectile.Damage * healPercentage);
        }
    }
}