using Enemies;
using UnityEngine;

namespace Effects
{
    public abstract class TimedEffect : BaseEffect
    {
        public float effectTime;
        public float tickTime;
        public int   amount;
        public abstract override void Apply(BaseEnemy enemy);
    }
}