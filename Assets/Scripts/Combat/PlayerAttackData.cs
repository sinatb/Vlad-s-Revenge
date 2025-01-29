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
            _isCritical = false;
            _criticalMultiplier = 0;
        }
        public TimedEffect Effect { get; private set; }
        public float LifeSteal { get; private set; }
        public float Damage { get; private set; }
        private bool _isCritical;
        private float _criticalMultiplier;
        public void AddDamage(float value)
        {
            if (_isCritical)
                Damage += value * _criticalMultiplier;
            else
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
            _isCritical = true;
            _criticalMultiplier = value;
            Damage *= _criticalMultiplier;
        }
    }
}