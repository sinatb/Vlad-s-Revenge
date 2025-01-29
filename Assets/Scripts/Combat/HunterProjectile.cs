using Enemies;
using Managers;
using UnityEngine;

namespace Combat
{
    public class HunterProjectile : PlayerProjectile
    {
        private bool _isSpecial;

        public void MakeSpecial()
        {
            _isSpecial = true;
        }
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (_isSpecial)
            {
                //TODO
            }
            else
            {
                if (other.gameObject.CompareTag("Wall"))
                {
                    gameObject.SetActive(false);
                }
                else if (other.gameObject.CompareTag("Enemy"))
                {
                    var enemy = other.gameObject.GetComponent<BaseEnemy>();
                    if (PlayerAttack.Effect != null)
                    {
                        enemy.AddEffect(PlayerAttack.Effect);
                    }
                    enemy.TakeDamage(PlayerAttack.Damage);
                    GameManager.Instance.player.Heal(PlayerAttack.Damage * PlayerAttack.LifeSteal);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}