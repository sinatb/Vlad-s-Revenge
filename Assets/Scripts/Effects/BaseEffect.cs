using Enemies;
using UnityEngine;

namespace Effects
{
    public abstract class BaseEffect : ScriptableObject
    {
        public abstract void Apply(BaseEnemy enemy);
    }
}