using System.Linq;
using Enemies;
using Managers;
using UnityEngine;

namespace Combat
{
    public class MageProjectile : PlayerProjectile
    {
        public int Aoe {get; set;}
        private void DamageNeighbouringEnemies(BaseEnemy enemy)
        {
            var position = new Vector2(
                    enemy.transform.position.x,
                    enemy.transform.position.y
                );
            var points = Physics2D.CircleCastAll(position, 5.0f, Vector2.zero);
            var ctr = 0;
            foreach (var rc in points.ToList())
            {
                if (ctr >= Aoe)
                    return;
                if (rc.transform.CompareTag("Enemy") && rc.transform != enemy.transform)
                {
                    var viewPos = Camera.main?.WorldToViewportPoint(rc.transform.position);
                    if (viewPos?.x is >= 0 and <= 1 && viewPos?.y is >= 0 and <= 1 && viewPos?.z > 0)
                    {
                        var enemyComponent = rc.transform.GetComponent<BaseEnemy>();
                        enemyComponent.TakeDamage(PlayerAttack.Damage);
                        ctr++;
                    }
                }
            }
        }

        protected override void OnCollisionEnter2D(Collision2D other)
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
                DamageNeighbouringEnemies(enemy);
                GameManager.Instance.player.Heal(PlayerAttack.Damage * PlayerAttack.LifeSteal);
                gameObject.SetActive(false);
            }
        }
    }
}