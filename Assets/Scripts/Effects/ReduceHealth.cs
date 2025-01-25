using Enemies;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "Effects/Instant/ReduceHealth",fileName = "ReduceHealth")]
    public class ReduceHealth : InstantEffect
    {
        public override void Apply(BaseEnemy enemy)
        {
            enemy.DecreaseHealthPercentage(amount);
        }
    }
}