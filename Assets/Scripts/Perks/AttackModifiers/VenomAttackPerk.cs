using Combat;
using Effects;
using UnityEngine;

namespace Perks.AttackModifiers
{
    [CreateAssetMenu (menuName = "Perks/AttackModifier/VenomAttackPerk", fileName = "VenomAttackPerk")]
    public class VenomAttackPerk : AttackModifierPerk
    {
        public TimedEffect venomEffect;
        public override void ModifyAttack(Player.Player player, PlayerAttackData attackData)
        {
            attackData.SetEffect(venomEffect);
        }
    }
}