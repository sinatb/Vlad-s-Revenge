using System;
using Effects;
using Enemies;
using Managers;
using UnityEngine;

namespace Combat
{
    public abstract class PlayerProjectile : Projectile
    {
        public PlayerAttackData PlayerAttack;

        public void SetPlayerAttackData(PlayerAttackData data)
        {
            PlayerAttack = data;
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }else if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.gameObject.GetComponent<BaseEnemy>();
                if (PlayerAttack.Effect != null)
                {
                    enemy.AddEffect(PlayerAttack.Effect);
                }
                enemy.TakeDamage(PlayerAttack.Damage);
                OnDamageDealt(enemy);
                GameManager.Instance.player.Heal(PlayerAttack.Damage*PlayerAttack.LifeSteal);
                gameObject.SetActive(false);
            } 
        }
        protected abstract void OnDamageDealt(BaseEnemy enemy);
    }
}