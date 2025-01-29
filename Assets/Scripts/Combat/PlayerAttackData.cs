using Effects;
using UnityEngine;

namespace Combat
{
    public class PlayerAttackData 
    {
        public PlayerAttackData(float damage)
        {
            Damage = damage;
            Effect = null;
            LifeSteal = 0;
        }
        public TimedEffect Effect { get; private set; }
        public float LifeSteal { get; private set; }
        public float Damage { get; private set; }
        
        public void AddDamage(float value)
        {
            Damage += value;
        }
        public void SetEffect(TimedEffect te)
        {
            Effect = te;
        }
        public void AddLifeSteal(float value)
        {
            LifeSteal += value;
        }
        public void MakeCritical(float value)
        {
            Damage *= value;
        }
    }
}