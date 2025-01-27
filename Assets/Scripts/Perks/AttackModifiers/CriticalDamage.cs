using Projectiles;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/AttackModifier/CriticalDamage", fileName = "CriticalDamage")]
    public class CriticalDamage : AttackModifierPerk
    {
        public float criticalChance;
        public float criticalMultiplier;
        public override void ModifyAttack(Player.Player player, PlayerProjectile projectile)
        {
            var rnd = Random.Range(0.0f, 1.0f);
            if (rnd > criticalChance) return;
            projectile.MakeCritical(criticalMultiplier);
        }
    }
}