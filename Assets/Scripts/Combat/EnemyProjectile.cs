using System;
using UnityEngine;

namespace Combat
{
    public class EnemyProjectile : Projectile
    {
        public float damage;
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var p = other.gameObject.GetComponent<Player.Player>();
                p.TakeDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}