using Perks.Interfaces;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/IncreaseDamageRelative", fileName = "IncreaseDamageRelative")]
    public class IncreaseDamageRelative : Perk, IStatsIncrease
    {
        public float amount;

        public void IncreaseStats(Player.Player p)
        {
            p.IncreaseMaximumDamage(amount);
        }
    }
}