using Enemies;
using UnityEngine;

namespace Effects
{
    public abstract class TimedEffect : BaseEffect
    {
        public int turns;
        public int amount;
        public abstract override void Apply(BaseEnemy enemy);
    }
}