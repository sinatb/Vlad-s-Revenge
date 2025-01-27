using Effects;
using UnityEngine;

namespace Projectiles
{
    public abstract class PlayerProjectile : Projectile
    {
        protected bool        IsCrit;
        protected TimedEffect Effect;
        public float ProjectileDamage => Damage;
        public void SetEffect(TimedEffect te)
        {
            Effect = te;
        }
        public void MakeCritical(float value)
        {
            IsCrit = true;
            Damage *= value;
        }
        private void OnDisable()
        {
            Effect = null;
        }
        protected abstract override void OnCollisionEnter2D(Collision2D other);
    }
}