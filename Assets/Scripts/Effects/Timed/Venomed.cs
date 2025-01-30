using Enemies;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu (menuName = "Effects/Timed/Venomed", fileName = "Venomed")]
    public class Venomed : TimedEffect
    {
        public override void Apply(BaseEnemy enemy)
        {
            enemy.TakeDamage(amount);
        }
    }
}