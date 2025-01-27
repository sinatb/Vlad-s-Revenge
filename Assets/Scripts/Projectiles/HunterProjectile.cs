using UnityEngine;

namespace Projectiles
{
    public class HunterProjectile : PlayerProjectile
    {
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            throw new System.NotImplementedException();
        }
    }
}