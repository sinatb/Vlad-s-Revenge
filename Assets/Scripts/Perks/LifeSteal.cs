using Perks.Interfaces;
using Projectiles;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/LifeSteal", fileName = "LifeSteal")]
    public class LifeSteal : Perk, IAttackModifier
    {
        public float healPercentage;
        public void ModifyAttack(Player.Player player, Projectile projectile)
        {
            player.Heal(projectile.Damage * healPercentage);
        }
    }
}