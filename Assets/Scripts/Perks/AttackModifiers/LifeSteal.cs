using Combat;
using UnityEngine;

namespace Perks.AttackModifiers
{
    [CreateAssetMenu(menuName = "Perks/AttackModifier/LifeSteal", fileName = "LifeSteal")]
    public class LifeSteal : AttackModifierPerk
    {
        public float healPercentage;
        public override void ModifyAttack(Player.Player player, PlayerAttackData attackData)
        {
            attackData.AddLifeSteal(healPercentage);
        }
    }
}