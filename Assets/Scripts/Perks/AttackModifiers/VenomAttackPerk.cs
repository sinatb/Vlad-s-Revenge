using Effects;
using Projectiles;
using UnityEngine;

namespace Perks.AttackModifiers
{
    [CreateAssetMenu (menuName = "Perks/AttackModifier/VenomAttackPerk", fileName = "VenomAttackPerk")]
    public class VenomAttackPerk : AttackModifierPerk
    {
        public TimedEffect venomEffect;
        public override void ModifyAttack(Player.Player player, PlayerProjectile projectile)
        {
            projectile.SetEffect(venomEffect);
        }
    }
}