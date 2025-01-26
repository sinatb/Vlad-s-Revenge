using Enemies;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "Effects/Instant/IncreaseBlood",fileName = "IncreaseBlood")]
    public class IncreaseBlood : InstantEffect
    {
        public override void Apply(BaseEnemy enemy)
        {
            enemy.IncreaseBlood(amount);
        }
    }
}