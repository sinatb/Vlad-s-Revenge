using Perks.Interfaces;
using UnityEngine;

namespace Perks.StatIncrease
{
    [CreateAssetMenu(menuName = "Perks/IncreaseHealth", fileName = "IncreaseHealth")]
    public class IncreaseHealthRelative : Perk, IStatsIncrease
    {
        public float amount;
        public void IncreaseStats(Player.Player p)
        {
            p.IncreaseMaximumHealth(amount);
        }
    }
}