using Combat;
using Enemies;
using Managers;
using UnityEngine;

namespace Player.Berserker
{
    public class BerserkerController : PlayerController
    {
        public override void Attack(PlayerAttackData data)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            var dir = (mouseWorldPos - transform.position).normalized;
            var hits = Physics2D.OverlapCircleAll(transform.position, 2.5f);

            foreach (Collider2D hit in hits)
            {
                var directionToTarget = 
                    ((Vector2)hit.transform.position - (Vector2)transform.position).normalized;
                
                var angle = Vector2.Angle(dir, directionToTarget);

                if (angle <= 40.0f && hit.CompareTag("Enemy")) 
                {
                    var enemy = hit.gameObject.GetComponent<BaseEnemy>();
                    if (data.Effect != null)
                    {
                        enemy.AddEffect(data.Effect);
                    }
                    enemy.TakeDamage(data.Damage);
                    GameManager.Instance.player.Heal(data.Damage*data.LifeSteal);
                }
            }
        }

        public override void Special()
        {
            
        }

        public override void AdditionalControls()
        {
            return;
        }
    }
}