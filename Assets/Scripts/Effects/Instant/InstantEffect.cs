using Enemies;
using UnityEngine;

namespace Effects
{
    public abstract class InstantEffect : BaseEffect
    {
		[Header("Amount of Impact")]
		public int amount;
        public abstract override void Apply(BaseEnemy enemy);
    }
}