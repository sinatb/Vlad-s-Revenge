using Perks.Interfaces;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/IncreaseHealth", fileName = "IncreaseHealth")]
    public class IncreaseHealthRelative : Perk, IStatsIncrease
    {
        public int amount;
        public void IncreaseStats(Player.Player p)
        {
            p.IncreaseHealth(amount);
        }
    }
}