using System.Linq;
using Enemies;
using UnityEngine;

namespace Projectiles
{
    public class MageProjectile : PlayerProjectile
    {
        public int Aoe {get; set;}
        protected override void OnDamageDealt(BaseEnemy enemy)
        {
            var position = new Vector2(
                    enemy.transform.position.x,
                    enemy.transform.position.y
                );
            var points = Physics2D.CircleCastAll(position, 5.0f, Vector2.zero);
            var ctr = 0;
            foreach (var rc in points.ToList())
            {
                if (rc.transform.CompareTag("Enemy") && rc.transform != enemy.transform)
                {
                    var enemyComponent = rc.transform.GetComponent<BaseEnemy>();
                    enemyComponent.TakeDamage(ProjectileDamage);
                    ctr++;
                }
                if (ctr == Aoe)
                    return;
            }
        }
    }
}