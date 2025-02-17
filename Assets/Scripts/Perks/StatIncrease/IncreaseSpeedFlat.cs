﻿using Perks.Interfaces;
using UnityEngine;

namespace Perks.StatIncrease
{
    [CreateAssetMenu(menuName = "Perks/IncreaseSpeedFlat", fileName = "IncreaseSpeedFlat")]
    public class IncreaseSpeedFlat : Perk, IStatsIncrease
    {
        public int amount;

        public void IncreaseStats(Player.Player p)
        {
            p.IncreaseMaximumSpeed(amount);
        }
    }
}