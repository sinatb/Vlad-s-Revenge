using Effects;
using Enemies;
using Managers;
using UnityEngine;

namespace Projectiles
{
    public class PlayerProjectile : Projectile
    {
        private TimedEffect _effect;
        private float       _lifeSteal;       
        public float ProjectileDamage => Damage;
        public void SetEffect(TimedEffect te)
        {
            _effect = te;
        }
        public void AddLifeSteal(float value)
        {
            _lifeSteal += value;
        }
        public void MakeCritical(float value)
        {
            Damage *= value;
        }
        private void OnDisable()
        {
            _effect = null;
            _lifeSteal = 0;
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }else if (other.gameObject.CompareTag("Enemy"))
            {
                if (_effect != null)
                {
                    other.gameObject.GetComponent<BaseEnemy>().AddEffect(_effect);
                }
                other.gameObject.GetComponent<BaseEnemy>().TakeDamage(Damage);
                GameManager.Instance.player.Heal(Damage*_lifeSteal);
                gameObject.SetActive(false);
            } 
        }
    }
}