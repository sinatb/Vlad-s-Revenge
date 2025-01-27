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
                if (ctr >= Aoe)
                    return;
                if (rc.transform.CompareTag("Enemy") && rc.transform != enemy.transform)
                {
                    var viewPos = Camera.main?.WorldToViewportPoint(rc.transform.position);
                    if (viewPos?.x is >= 0 and <= 1 && viewPos?.y is >= 0 and <= 1 && viewPos?.z > 0)
                    {
                        var enemyComponent = rc.transform.GetComponent<BaseEnemy>();
                        enemyComponent.TakeDamage(ProjectileDamage);
                        ctr++;
                    }
                }
            }
        }
    }
}