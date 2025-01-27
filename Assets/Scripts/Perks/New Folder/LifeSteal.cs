using Perks.Interfaces;
using Projectiles;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/AttackModifier/LifeSteal", fileName = "LifeSteal")]
    public class LifeSteal : AttackModifierPerk
    {
        public float healPercentage;
        public override void ModifyAttack(Player.Player player, PlayerProjectile projectile)
        {
            player.Heal(projectile.ProjectileDamage * healPercentage);
        }
    }
}