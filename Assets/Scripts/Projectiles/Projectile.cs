using System;
using Effects;
using Enemies;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        private Vector2     _direction;
        private float       _speed;
        protected float     Damage;
        public void Setup(Vector2 dir, float speed, float damage)
        {
            _direction = dir;
            _speed = speed;
            Damage = damage;
        }
        private void Update()
        {
            transform.position += (Vector3)(_direction * (_speed * Time.deltaTime));
        }
        protected abstract void OnCollisionEnter2D(Collision2D other);
    }
}