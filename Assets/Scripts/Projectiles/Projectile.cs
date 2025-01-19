using System;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private Vector2 _direction;
        private float   _speed;
        public void Setup(Vector2 dir, float speed)
        {
            _direction = dir;
            _speed = speed;
        }
        
        private void Update()
        {
            transform.position += (Vector3)(_direction * (_speed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}