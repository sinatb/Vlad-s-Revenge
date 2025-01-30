using System.Collections;
using System.Collections.Generic;
using Combat;
using Enemies;
using Managers;
using UnityEngine;

namespace Player.Berserker
{
    public class BerserkerController : PlayerController
    {
        private const float InvincibilityTime = 5.0f;
        public override void Attack(PlayerAttackData data)
        {
            var dir = GetMouseVector();
            
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
        private IEnumerator InvinicibilityCoroutine()
        {
            yield return new WaitForSeconds(InvincibilityTime);
            GameManager.Instance.player.isInvincible = false;
        }
        public override void Special()
        {
            var player = GetComponent<Player>();
            player.isInvincible = true;
            StartCoroutine(InvinicibilityCoroutine());
        }

        public override void AdditionalControls()
        {
            return;
        }
    }
}