using Combat;
using Perks.Interfaces;
using UnityEngine;

namespace Perks.AttackModifiers
{
    public abstract class AttackModifierPerk : Perk, IAttackModifier
    {
        [Header("Priority, 0 = Max")]
        public byte priority;
        public abstract void ModifyAttack(Player.Player player, PlayerAttackData attackData);
    }
}