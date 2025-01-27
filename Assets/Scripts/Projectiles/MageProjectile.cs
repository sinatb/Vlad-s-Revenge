using Enemies;
using UnityEngine;

namespace Projectiles
{
    public class MageProjectile : PlayerProjectile
    {
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }else if (other.gameObject.CompareTag("Enemy"))
            {
                if (Effect != null)
                {
                    other.gameObject.GetComponent<BaseEnemy>().AddEffect(Effect);
                }
                other.gameObject.GetComponent<BaseEnemy>().TakeDamage(Damage);
                gameObject.SetActive(false);
            }       
        }
    }
}